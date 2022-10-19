using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.DTO;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;
using Storage.WebApi.Helpers;

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
        private readonly BaseControllerOperationsStrategy<AreaDTO> _baseOperations;

        public AreaController(
            IUnitOfWorkAsync uw, ILogger<AreaController> logger,
            IMapper mapper, IAreaBuilder areaBuilder)
        {
            _uw = uw;
            _logger = logger;
            _mapper = mapper;
            _areaBuilder = areaBuilder;

            _baseOperations = new BaseControllerOperationsStrategy<AreaDTO>(_uw, _logger, _mapper, this);
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

            return await _baseOperations.BasicPost(_mapper.Map<AreaDTO>(newArea),
                            async () => await _ar.AddAsync(newArea),
                            nameof(Get));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AreaDTO value) =>

            await _baseOperations.BasicPut(() =>
                _ar.UpdateAsync(new Area() { Id = value.Id, Name = value.Name }));


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) =>
             await _baseOperations.BasicDelete(() => _ar.DeleteAsync(new Area() { Id = id }));

    }
}