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
        public override async Task<ActionResult> Post([FromBody] StorageItemDTO value)
        {
            if (value is null)
                return BadRequest();

            StorageItemDTO outVal;
            StorageItem result;

            try
            {
                result = await _sr.AddAsync(Mapper.Map<StorageItem>(value));
                await Uw.Commit();
            }
            catch (StorageItemDoesNotFitInCell ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            outVal = Mapper.Map<StorageItemDTO>(result);
            return Ok(outVal);
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> Put(int id, [FromBody] StorageItemDTO value)
        {
            value.Id = id;
            try
            {
                await _sr.UpdateAsync(Mapper.Map<StorageItem>(value));
                await Uw.Commit();
            }
            catch (NotFound<StorageItem> ex)
            {
                return NotFound();
            }
            catch (StorageItemDoesNotFitInCell ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _sr.DeleteAsync(new StorageItem() { Id = id });
                await Uw.Commit();
            }
            catch (NoCascadeDeletionException<Product> ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFound<Product> ex)
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
