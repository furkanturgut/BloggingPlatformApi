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

    }
}
