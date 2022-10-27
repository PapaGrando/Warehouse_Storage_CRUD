using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.Core.Interfaces;
using Storage.WebApi.Filters;

namespace Storage.Core.Controllers.Products
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
        [ExceptionFilter]
        public override async Task<IEnumerable<ProductDTO>> GetAll() =>
            await BaseControllerOperations.BasicGetAll(async () => await _pRepo.GetAllAsync());

        /// <summary>
        /// Returning categories with parameters.
        /// Offset - Offset in database table from start. PageSize - Object list size
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet]
        [ExceptionFilter]
        public override async Task<IEnumerable<ProductDTO>> GetListWithParameters([FromQuery] QuerySettings query) => 
            await BaseControllerOperations.BasicGetAll(async () => await _pRepo.GetSelectedAsync(query));

        /// <summary>
        /// Returning Category with ID
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet("{id}")]
        [ExceptionFilter]
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
        [ExceptionFilter]
        public override async Task<ActionResult> Post([FromBody] ProductDTO value) =>
            await BaseControllerOperations.BasicPost(value,
                async () => await _pRepo.AddAsync(new Product() { Id = 0, Name = value.Name }), 
                nameof(Get));

        /// <summary>
        /// Updating Product with ID
        /// </summary>
        [HttpPut("{id}")]
        [ExceptionFilter]
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
        [ExceptionFilter]
        public override async Task<ActionResult> Delete(int id) =>
           await BaseControllerOperations.BasicDelete(() => _pRepo.DeleteAsync(new Product() { Id = id }));
    }
}
