using CMS.application.Categories.Interfaces;
using CMS.application.Categories.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;

        }

        /// <summary>
        /// www.test.com/api/category/Create
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryCreationDto category)
        {
            try
            {
                var categoryResult = await _categoryService.Create(category);
                return StatusCode((int)HttpStatusCode.Created, categoryResult);
            }
            catch (Exception e)
            {
                _logger.LogError("CategoryController error: CreateAsync " + e.Message);
                return BadRequest("Bad Request, contact administrator");
            }
            
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CategoryReadDto category)
        {
            try
            {
                var categoryResult = await _categoryService.Update(category);
                return new OkObjectResult(categoryResult);
            }
            catch (Exception e)
            {
                _logger.LogError("CategoryController error: Update " + e.Message);
                return BadRequest("Bad Request, contact administrator");
            }
            
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _categoryService.Delete(id);

                return new OkObjectResult(new { deleted = result });
            }
            catch (Exception e)
            {
                _logger.LogError("CategoryController error: Delete " + e.Message);
                return BadRequest("Bad Request, contact administrator");
            }
            
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categories = await _categoryService.GetAll();

                return new OkObjectResult(categories);
            }
            catch (Exception e)
            {
                _logger.LogError("CategoryController error: GetAll " + e.Message);
                return BadRequest("Bad Request, contact administrator");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var category = await _categoryService.Get(id);

                return new OkObjectResult(category);
            }
            catch(InvalidOperationException e)
            {
                _logger.LogInformation($"Category with id {id} not found " + e.Message);
                return NotFound($"Category with id {id} not found");
            }
            catch (Exception e)
            {
                _logger.LogError("CategoryController error: GetById " + e.Message);
                return BadRequest("Bad Request, contact administrator");
            }
        }
    }
}