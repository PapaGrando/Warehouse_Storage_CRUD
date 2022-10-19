using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.Core.Controllers
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class CellsController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _uw;
        private readonly ILogger<CellsController> _logger;
        private readonly IMapper _mapper;
        private ICellRepoAsync _cr => _uw.Cells;

        public CellsController(IUnitOfWorkAsync uw, ILogger<CellsController> logger, IMapper mapper)
        {
            _uw = uw;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CellDTOInfoReadOnly>> Get(int id)
        {
            var result = await _cr.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return new CellDTOInfoReadOnly()
            {
                Id = result.Id,
                SubAreaId = result.SubAreaId,
                SubAreaLengthX = result.SubAreaLengthX,
                SubAreaWidthY = result.SubAreaWidthY,
                SubAreaHeigthZ = result.SubAreaHeigthZ,
                Items = result.Items?.Select(x => _mapper.Map<StorageItemDTO>(x)).ToList()
            };
        }

        [HttpGet("all/{subAreaid}")]
        public async Task<IEnumerable<CellDTOInfoReadOnly>> GetAll(int subAreaid)
        {
            var data = await _cr.GetAllInSubAreaAsync(subAreaid);

            return data.Entities.Select(x => new CellDTOInfoReadOnly()
            {
                Id = x.Id,
                SubAreaId = x.SubAreaId,
                SubAreaLengthX = x.SubAreaLengthX,
                SubAreaWidthY = x.SubAreaWidthY,
                SubAreaHeigthZ = x.SubAreaHeigthZ,
                Items = x.Items?.Select(x => _mapper.Map<StorageItemDTO>(x)).ToList()
            }).ToArray();
        }

        [HttpGet]
        public async Task<IEnumerable<CellDTOInfoReadOnly>>
            GetListWithParameters([FromQuery] QuerySettingsWithIdSubArea query)
        {
            var result = await _cr.GetSelectedAsync(query);

            return result.Entities.Select(x => new CellDTOInfoReadOnly()
            {
                Id = x.Id,
                SubAreaId = x.SubAreaId,
                SubAreaLengthX = x.SubAreaLengthX,
                SubAreaWidthY = x.SubAreaWidthY,
                SubAreaHeigthZ = x.SubAreaHeigthZ,
                Items = x.Items?.Select(x => _mapper.Map<StorageItemDTO>(x)).ToList()
            }).ToArray();
        }
    }
}
