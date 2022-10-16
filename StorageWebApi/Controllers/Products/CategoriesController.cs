﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Storage.WebApi.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.WebApi.Controllers.Products
{
    [Route("api/Products/[controller]")]
    [ApiController]
    public class CategoriesController : StorageBaseController<ProductCategoryDTO, CategoriesController>
    {
        private IProductCategoryRepoAsync _pcRepo => Uw.ProductCategories;

        public CategoriesController(IUnitOfWorkAsync uw, ILogger<CategoriesController> logger, IMapper mapper)
            : base(uw, logger, mapper) { }

        // GET: api/<Categories>
        /// <summary>
        /// Returning All categories
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet("all")]
        public override async Task<IEnumerable<ProductCategoryDTO>> GetAll()
        {
            var data = await _pcRepo.GetAllAsync();

            return data.Select(x => Mapper.Map<ProductCategoryDTO>(x)).ToArray();
        }

        /// <summary>
        /// Returning categories with parameters.
        /// Offset - Offset in database table from start. PageSize - Object list size
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet]
        public override async Task<IEnumerable<ProductCategoryDTO>> GetListWithParameters([FromQuery] QuerySettings query)
        {
            var result = await _pcRepo.GetSelectedAsync(query);

            return result.Select(x => Mapper.Map<ProductCategoryDTO>(x)).ToArray();
        }

        /// <summary>
        /// Returning Category with ID
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpGet("{id}")]
        public override async Task<ActionResult<object>> Get(int id)
        {
            var result = await _pcRepo.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return new ProductCategoryInfoReadOnly()
            {
                Id = result.Id,
                Name = result.Name,
                Products = result.Products.Select(x =>
                Mapper.Map<ProductDTO>(x))
            };
        }

        // POST api/<Categories>
        /// <summary>
        /// Creating Category with Name
        /// </summary>
        /// <returns>ProductCategoryDTO</returns>
        [HttpPost]
        public override async Task<ActionResult> Post([FromBody] ProductCategoryDTO value)
        {
            if (value is null)
                return BadRequest();

            ProductCategoryDTO outVal;
            ProductCategory result;

            try
            {
                result = await _pcRepo.AddAsync(new ProductCategory() { Id = 0, Name = value.Name });
                await Uw.Commit();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest();
            }

            outVal = Mapper.Map<ProductCategoryDTO>(result);
            return CreatedAtAction(nameof(Get), outVal);
        }

        // PUT api/<Categories>/5
        /// <summary>
        /// Updating Category with ID
        /// </summary>
        [HttpPut("{id}")]
        public override async Task<ActionResult> Put(int id, [FromBody] ProductCategoryDTO value)
        {
            try
            {
                await _pcRepo.UpdateAsync(new ProductCategory() { Id = id, Name = value.Name });
                await Uw.Commit();
            }
            catch (NotFound<ProductCategory> ex)
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

        // DELETE api/<Categories>/5
        /// <summary>
        ///  Deleting Category with ID.
        ///  There must be no products in the category before deletion
        /// </summary>
        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _pcRepo.DeleteAsync(new ProductCategory() { Id = id });
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