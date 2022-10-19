using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;

namespace Storage.WebApi.Controllers.Products
{
    [Route("api/Products")]
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
        public override async Task<ActionResult> Post([FromBody] ProductDTO value) =>
            await BaseControllerOperations.BasicPost(value,
                async () => await _pRepo.AddAsync(new Product() { Id = 0, Name = value.Name }), 
                nameof(Get));

        /// <summary>
        /// Updating Product with ID
        /// </summary>
        [HttpPut("{id}")]
        public override async Task<ActionResult> Put(int id, [FromBody] ProductDTO value)
        {
            value.Id = id;
            return await BaseControllerOperations.BasicPut(() => _pRepo.UpdateAsync(Mapper.Map<Product>(value)));
        }

        /// <summary>
        ///  Deleting Product with ID.
        ///  There must be no products in the category before deletion
        /// </summary>
        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id) =>
           await BaseControllerOperations.BasicDelete(() => _pRepo.DeleteAsync(new Product() { Id = id }));
    }
}
