using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BloggingPlatformApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<CategoryDto>))]
        public ActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{CategoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public ActionResult GetCategory(int CategoryId)
        {
            if (!_categoryRepository.CategoryExist(CategoryId))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(CategoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("article/{CategoryId}")]
        [ProducesResponseType(200, Type = typeof(ArticleDto))]
        [ProducesResponseType(400)]
        public ActionResult GetArticleByCategory(int CategoryId)
        {
            if (!_categoryRepository.CategoryExist(CategoryId))
            {
                return NotFound();
            }
            var articles = _mapper.Map<ICollection<ArticleDto>>(_categoryRepository.GetArticleByCategory(CategoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(articles);



        }


        [HttpGet("category/{ArticleId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public ActionResult GetArticleCategory(int ArticleId)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetArticleCategory(ArticleId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory(CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest(ModelState);
            }
            var categories = _categoryRepository.GetCategories().FirstOrDefault(c => c.Name.Trim().ToUpper() == category.Name.Trim().ToUpper());
            if (categories != null)
            {
                ModelState.AddModelError("", "Category Alreadt Exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CategoryMap = _mapper.Map<Category>(category);
            if (!_categoryRepository.CreateCategory(CategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly Created");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest(ModelState);
            }
            var UpdatedCategory = _mapper.Map<Category>(category);
            if (!_categoryRepository.UpdateCategory(UpdatedCategory))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int CategoryId)
        {
            if (!_categoryRepository.CategoryExist(CategoryId))
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategory(CategoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
