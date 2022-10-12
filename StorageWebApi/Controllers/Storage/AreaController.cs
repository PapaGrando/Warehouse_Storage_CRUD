using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.DTO;
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
    public class AreaController : ControllerBase
    {
        private IAreaRepoAsync _ar => _uw.Areas;

        private readonly IUnitOfWorkAsync _uw;
        private readonly ILogger<AreaController> _logger;
        private readonly IMapper _mapper;
        private IAreaBuilder _areaBuilder;
        public AreaController(
            IUnitOfWorkAsync uw, ILogger<AreaController> logger,
            IMapper mapper, IAreaBuilder areaBuilder)
        {
            _uw = uw;
            _logger = logger;
            _mapper = mapper;
            _areaBuilder = areaBuilder;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTOInfoReadOnly>> Get(int id)
        {
            var result = await _ar.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            var outa = new AreaDTOInfoReadOnly()
            {
                Id = result.Id,
                Name = result.Name,
                SubAreas = result.SubAreas.Select(x => _mapper.Map<SubAreaDTOReadOnlyInfo>(x))
            };

            return outa;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AreaDTO>> GetAll()
        {
            var data = await _ar.GetAllAsync();

            return data.Select(x => _mapper.Map<AreaDTO>(x)).ToArray();
        }

        [HttpGet]
        public async Task<IEnumerable<AreaDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _ar.GetSelectedAsync(query);

            return result.Select(x => _mapper.Map<AreaDTO>(x)).ToArray();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AreaConfiguration value)
        {
            var newArea = _areaBuilder
                            .ApplyConfig(value)
                            .Build();

            AreaDTO outVal;
            Area result;

            try
            {
                result = await _ar.AddAsync(newArea);
                await _uw.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            outVal = new AreaDTO() { Id = result.Id, Name = result.Name };
            return CreatedAtAction(nameof(Get), outVal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AreaDTO value)
        {
            value.Id = id;
            try
            {
                await _ar.UpdateAsync(new Area() { Id = value.Id, Name = value.Name});
                await _uw.Commit();
            }
            catch (NotFound<Area> ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _ar.DeleteAsync(new Area() { Id = id });
                await _uw.Commit();
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
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }

            return Ok();
        }
    }
}
