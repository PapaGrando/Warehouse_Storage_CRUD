using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.WebApi.Controllers.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class ItemsController : StorageBaseController<StorageItemDTO, ItemsController>
    {
        private IStorageItemRepoAsync _sr => Uw.StorageItems;
        public ItemsController(IUnitOfWorkAsync uw, ILogger<ItemsController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        [HttpGet("{id}")]
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

        [HttpGet("all")]
        public override async Task<IEnumerable<StorageItemDTO>> GetAll()
        {
            var data = await _sr.GetAllAsync();

            return data.Select(x => Mapper.Map<StorageItemDTO>(x)).ToArray();
        }

        [HttpGet]
        public override async Task<IEnumerable<StorageItemDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _sr.GetSelectedAsync(query);

            return result.Select(x => Mapper.Map<StorageItemDTO>(x)).ToArray();
        }

        [HttpPost]
        public override async Task<ActionResult> Post([FromBody] StorageItemDTO value) =>
            await BaseControllerOperations.BasicPost(value, 
                async () => await _sr.AddAsync(Mapper.Map<StorageItem>(value)), 
                nameof(Get));

        [HttpPut("{id}")]
        public override async Task<ActionResult> Put(int id, [FromBody] StorageItemDTO value)
        {
            value.Id = id;
            return await BaseControllerOperations.BasicPut(() => _sr.UpdateAsync(Mapper.Map<StorageItem>(value)));
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id) =>
            await BaseControllerOperations.BasicDelete(() => _sr.DeleteAsync(new StorageItem() { Id = id }));
    }
}
