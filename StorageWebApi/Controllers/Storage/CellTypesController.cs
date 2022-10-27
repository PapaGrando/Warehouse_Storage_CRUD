using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.Core.Controllers.Storage
{

    [Route("api/Storage/[controller]")]
    [ApiController]
    public class CellTypesController : StorageBaseController<CellTypeDTO, CellTypesController>
    {
        private ICellTypeRepoAsync _cr => Uw.CellTypes;

        public CellTypesController(IUnitOfWorkAsync uw, ILogger<CellTypesController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        /// <summary>
        /// Returns ALL celltypes in database. Use wisely
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ExceptionFilter]
        public async override Task<IEnumerable<CellTypeDTO>> GetAll() =>
             await BaseControllerOperations.BasicGetAll(async () => await _cr.GetAllAsync());

        /// <summary>
        /// Returns list of celltypes with setted parameters in query
        /// </summary>
        [HttpGet]
        [ExceptionFilter]
        public async override Task<IEnumerable<CellTypeDTO>> GetListWithParameters([FromQuery] QuerySettings query)=> 
            await BaseControllerOperations.BasicGetAll(async () => await _cr.GetSelectedAsync(query));

        /// <summary>
        /// Returns detailed info of cell type
        /// </summary>
        /// <param name="id">id of cell type</param>
        [HttpGet("{id}")]
        [ExceptionFilter]
        public async override Task<ActionResult<object>> Get(int id)
        {
            var result = await _cr.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return new CellTypeReadOnlyInfoDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Length = result.Length,
                Height = result.Height,
                Width = result.Width,
                MaxWeight = result.MaxWeight,
                SubAreas = result.SubAreas?.Select(x => Mapper.Map<SubAreaDTOReadOnlyInfo>(x))
            };
        }

        /// <summary>
        /// Creates new celltype. Returns created object
        /// </summary>
        /// <param name="value">Data to create</param>
        [HttpPost]
        [ExceptionFilter]
        public async override Task<ActionResult> Post([FromBody] CellTypeDTO value) =>
            await BaseControllerOperations.BasicPost(
                value, 
                async () => await _cr.AddAsync(Mapper.Map<CellType>(value)),
                nameof(Get));

        /// <summary>
        /// Updating cell type with id
        /// </summary>
        /// <param name="id">id of cell type</param>
        /// <param name="value">Data to change</param>
        [HttpPut("{id}")]
        [ExceptionFilter]
        public async override Task<ActionResult> Put(int id, [FromBody] CellTypeDTO value) =>
            await BaseControllerOperations.BasicPut(() =>
            {
                value.Id = id;
                return _cr.UpdateAsync(Mapper.Map<CellType>(value));
            });

        /// <summary>
        /// Deleting celltype with id. Must be cleared from products
        /// </summary>
        /// <param name="id">id of cell type</param>
        [HttpDelete("{id}")]
        [ExceptionFilter]
        public async override Task<ActionResult> Delete(int id) =>
            await BaseControllerOperations.BasicDelete(() => _cr.DeleteAsync(new CellType() { Id = id }));
    }
}
