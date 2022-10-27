using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Helpers;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.Core.Controllers.Storage
{
    [ExceptionFilter]
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

        /// <summary>
        /// Get Area by id with all Subareas, Cells and StorageItems in Area. Its a heavy request
        /// </summary>
        /// <param name="id">Id of Area</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returning all Areas names and id
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IEnumerable<AreaDTO>> GetAll() =>
            await _baseOperations.BasicGetAll(async () => await _ar.GetAllAsync());

        /// <summary>
        /// Returning list Areas with parameters
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<AreaDTO>> GetListWithParameters([FromQuery] QuerySettings query) =>
            await _baseOperations.BasicGetAll(async () => await _ar.GetSelectedAsync(query));

        /// <summary>
        /// Creating new area with Configuration 
        /// </summary>
        /// <param name="value">
        /// Id - ignored <br/>
        /// Name - Set Name of Area <br/>
        /// SubAreaConfiguration - SubArea and Cells configuration: <br/>
        /// Length(1 - 1000), Width(1 - 2), Height(from 85) - size SubArea in Cells <br/>
        /// CellTypeId - id of type Cell (see CellType)
        /// </param>
        /// <returns></returns>
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

        /// <summary>
        /// Change name of Area
        /// </summary>
        /// <param name="id">Area id</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AreaDTO value) =>

            await _baseOperations.BasicPut(() =>
                _ar.UpdateAsync(new Area() { Id = value.Id, Name = value.Name }));

        /// <summary>
        /// Removes Area. its Delete all SubAreas and Cells in this Area. 
        /// Must cleared from StorageItems before using.
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) =>
             await _baseOperations.BasicDelete(() => _ar.DeleteAsync(new Area() { Id = id }));
    }
}