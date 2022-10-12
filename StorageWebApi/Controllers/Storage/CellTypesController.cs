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
        private ICellTypeRepoAsync _sr => Uw.CellTypes;

        public CellTypesController(IUnitOfWorkAsync uw, ILogger<CellTypesController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        [HttpGet("all")]
        public async override Task<IEnumerable<CellTypeDTO>> GetAll()
        {
            var data = await _sr.GetAllAsync();

            return data.Select(x => Mapper.Map<CellTypeDTO>(x)).ToArray();
        }

        [HttpGet]
        public async override Task<IEnumerable<CellTypeDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _sr.GetSelectedAsync(query);

            return result.Select(x => Mapper.Map<CellTypeDTO>(x)).ToArray();
        }

        [HttpGet("{id}")]
        public async override Task<ActionResult<object>> Get(int id)
        {
            var result = await _sr.GetByIdAsync(id);

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
        public async override Task<ActionResult> Post([FromBody] CellTypeDTO value)
        {
            if (value is null)
                return BadRequest();

            value.Id = 0;
            CellTypeDTO outVal;
            CellType result;

            try
            {
                result = await _sr.AddAsync(Mapper.Map<CellType>(value));
                await Uw.Commit();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            outVal = Mapper.Map<CellTypeDTO>(result);
            return CreatedAtAction(nameof(Get), outVal);
        }
        [HttpPut("{id}")]
        public async override Task<ActionResult> Put(int id, [FromBody] CellTypeDTO value)
        {
            value.Id = id;
            try
            {
                await _sr.UpdateAsync(Mapper.Map<CellType>(value));
                await Uw.Commit();
            }
            catch (NotFound<CellType> ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async override Task<ActionResult> Delete(int id)
        {
            try
            {
                await _sr.DeleteAsync(new CellType() { Id = id });
                await Uw.Commit();
            }
            catch (NoCascadeDeletionException<ProductCategory> ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFound<ProductCategory> ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }

            return Ok();
        }
    }
}
