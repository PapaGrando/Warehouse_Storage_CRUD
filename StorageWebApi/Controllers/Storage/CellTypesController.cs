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
    public class CellTypesController : StorageBaseController<CellTypeDTO, CellTypesController>
    {
        private ICellTypeRepoAsync _cr => Uw.CellTypes;

        public CellTypesController(IUnitOfWorkAsync uw, ILogger<CellTypesController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        [HttpGet("all")]
        public async override Task<IEnumerable<CellTypeDTO>> GetAll()
        {
            var data = await _cr.GetAllAsync();

            return data.Select(x => Mapper.Map<CellTypeDTO>(x)).ToArray();
        }

        [HttpGet]
        public async override Task<IEnumerable<CellTypeDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _cr.GetSelectedAsync(query);

            return result.Select(x => Mapper.Map<CellTypeDTO>(x)).ToArray();
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async override Task<ActionResult> Post([FromBody] CellTypeDTO value) =>
            await BaseControllerOperations.BasicPost(
                value, 
                async () => await _cr.AddAsync(Mapper.Map<CellType>(value)),
                nameof(Get));

        [HttpPut("{id}")]
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
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async override Task<ActionResult> Delete(int id) =>
            await BaseControllerOperations.BasicDelete(() => _cr.DeleteAsync(new CellType() { Id = id }));
    }
}
