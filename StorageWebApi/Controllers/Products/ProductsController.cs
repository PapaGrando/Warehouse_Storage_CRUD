using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;

namespace Storage.WebApi.Controllers.Products
{
    [Route("api/Products/Items")]
    [ApiController]
    public class ProductsController : StorageBaseController<ProductDTO, ProductsController>
    {
        private IProductRepoAsync _pRepo => Uw.Products;

        public ProductsController(IUnitOfWorkAsync uw, ILogger<ProductsController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }
        // GET: api/<Categories>
        /// <summary>
        /// Returning All Products
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet("all")]
        public override async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var data = await _pRepo.GetAllAsync();

            return data.Select(x => Mapper.Map<ProductDTO>(x)).ToArray();
        }

        /// <summary>
        /// Returning categories with parameters.
        /// Offset - Offset in database table from start. PageSize - Object list size
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet]
        public override async Task<IEnumerable<ProductDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _pRepo.GetSelectedAsync(query);

            return result.Select(x => Mapper.Map<ProductDTO>(x)).ToArray();
        }

        /// <summary>
        /// Returning Category with ID
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<object>> Get(int id)
        {
            var result = await _pRepo.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(Mapper.Map<ProductDTOInfoReadOnly>(result));
        }

        /// <summary>
        /// Creating Product with Parameters
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpPost]
        public override async Task<ActionResult> Post([FromBody] ProductDTO value)
        {
            if (value is null)
                return BadRequest();

            ProductDTO outVal;
            Product result;

            try
            {
                result = await _pRepo.AddAsync(new Product() { Id = 0, Name = value.Name });
                await Uw.Commit();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            outVal = Mapper.Map<ProductDTO>(result);
            return CreatedAtAction(nameof(Get), outVal);
        }

        /// <summary>
        /// Updating Product with ID
        /// </summary>
        [HttpPut("{id}")]
        public override async Task<ActionResult> Put(int id, [FromBody] ProductDTO value)
        {
            value.Id = id;
            try
            {
                await _pRepo.UpdateAsync(Mapper.Map<Product>(value));
                await Uw.Commit();
            }
            catch (NotFound<Product> ex)
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

        /// <summary>
        ///  Deleting Product with ID.
        ///  There must be no products in the category before deletion
        /// </summary>
        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _pRepo.DeleteAsync(new Product() { Id = id });
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
