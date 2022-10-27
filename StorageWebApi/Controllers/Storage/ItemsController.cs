using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.Core.Controllers.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class ItemsController : StorageBaseController<StorageItemDTO, ItemsController>
    {
        private IStorageItemRepoAsync _sr => Uw.StorageItems;
        public ItemsController(IUnitOfWorkAsync uw, ILogger<ItemsController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        /// <summary>
        /// Returns detailed info of StorageItem.
        /// </summary>
        /// <param name="id">StorageItem id</param>
        [HttpGet("{id}")]
        [ExceptionFilter]
        public override async Task<ActionResult<object>> Get(int id)
        {
            var result = await _sr.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(new StorageItemDTOInfoReadOnly
            {
                Id = result.Id,
                Product = Mapper.Map<ProductDTOItemInfoReadOnly>(result.Product),
                Cell = Mapper.Map<CellDTOShortInfoReadOnly>(result.Cell),
                AddTime = result.AddTime
            });
        }

        /// <summary>
        /// Returns ALL StorageItems in database. Use wisely
        /// </summary>
        [HttpGet("all")]
        [ExceptionFilter]
        public override async Task<IEnumerable<StorageItemDTO>> GetAll() =>
            await BaseControllerOperations.BasicGetAll(async () => await _sr.GetAllAsync());

        /// <summary>
        /// Returns StorageItems with parameters. Use for pagination. See headers parameters
        /// </summary>
        [HttpGet]
        [ExceptionFilter]
        public override async Task<IEnumerable<StorageItemDTO>> GetListWithParameters([FromQuery] QuerySettings query) =>
            await BaseControllerOperations.BasicGetAll(async () => await _sr.GetSelectedAsync(query));

        /// <summary>
        /// Creating new StorageItem add try add to target Cell
        /// </summary>
        /// <param name="value">
        /// Id - ignored <br/>
        /// if AddTime is null, ist will be generated on server side
        /// </param>
        [HttpPost]
        [ExceptionFilter]
        public override async Task<ActionResult> Post([FromBody] StorageItemDTO value)
        {
            value.AddTime = value.AddTime ?? DateTime.UtcNow;

            return await BaseControllerOperations.BasicPost(value,
                            async () => await _sr.AddAsync(Mapper.Map<StorageItem>(value)),
                            nameof(Get));
        }

        /// <summary>
        /// Try changing target StorageItem or replace to target cell
        /// </summary>
        /// <param name="value">
        /// if AddTime is null, ist will be generated on server side
        /// </param>
        /// <param name="id">Storage item to change</param>
        [HttpPut("{id}")]
        [ExceptionFilter]
        public override async Task<ActionResult> Put(int id, [FromBody] StorageItemDTO value)
        {
            value.Id = id;
            value.AddTime = value.AddTime ?? DateTime.UtcNow;
            return await BaseControllerOperations.BasicPut(() => _sr.UpdateAsync(Mapper.Map<StorageItem>(value)));
        }

        /// <summary>
        /// Deletes target StorageItem
        /// </summary>
        [HttpDelete("{id}")]
        [ExceptionFilter]
        public override async Task<ActionResult> Delete(int id) =>
            await BaseControllerOperations.BasicDelete(() => _sr.DeleteAsync(new StorageItem() { Id = id }));
    }
}
