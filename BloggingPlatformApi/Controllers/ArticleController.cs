using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using BloggingPlatformApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatformApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ArticleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _repository;
        private readonly IWriterRepository _writerRepos;
        private readonly ICategoryRepository _categoryRepos;

        public ArticleController(IMapper mapper, IArticleRepository repository, IWriterRepository writerepos, ICategoryRepository categoryRepos)
        {
            this._mapper = mapper;
            this._repository = repository;
            this._writerRepos = writerepos;
            this._categoryRepos = categoryRepos;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ArticleDto>))]

        public IActionResult GetArticles()
        {
            var articles = _mapper.Map<ICollection<ArticleDto>>(_repository.GetArticles());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(articles);
        }

        [HttpGet("{ArticleId}")]
        [ProducesResponseType(200, Type = typeof(ArticleDto))]
        [ProducesResponseType(400)]
        public IActionResult GetArticle(int ArticleId)
        {
            if (!_repository.ArticleExist(ArticleId))
            {
                return NotFound();
            }

            var article = _mapper.Map<ArticleDto>(_repository.GetArticle(ArticleId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(article);
        }

        [HttpGet("tags/{ArticleId}")]
        [ProducesResponseType(200, Type= typeof(ICollection<TagDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetArticleTags (int ArticleId)
        {
            if (!_repository.ArticleExist(ArticleId))
            {
                return NotFound();
            }
            var tags= _mapper.Map<ICollection<TagDto>>(_repository.GetArticleTags(ArticleId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTag([FromQuery] int CategoryId, [FromQuery] int WriterId ,[FromBody] ArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(ModelState);
            }
            var articles = _repository.GetArticles().FirstOrDefault(a => a.Title.Trim().ToUpper() == article.Title.Trim().ToUpper());
            if (articles != null)
            {
                ModelState.AddModelError("", "Article Already Exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var TagMap = _mapper.Map<Article>(article);
            TagMap.Writer = _writerRepos.GetWriter(WriterId);
            TagMap.Category = _categoryRepos.GetCategory(CategoryId);
            if (!_repository.CreateArticle(TagMap))
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
        public IActionResult UpdateArticle(ArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(ModelState);
            }
            var UpdatedArticle= _mapper.Map<Article>(article);
            if (!_repository.UpdateArticle(UpdatedArticle))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{ArticleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int ArticleId)
        {
            if (!_repository.ArticleExist(ArticleId))
            {
                return NotFound();
            }
            var article = _repository.GetArticle(ArticleId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.DeleteArticle(article))
            {
                ModelState.AddModelError("", "Something went wrong while removing Article");
                return StatusCode(500, ModelState);
            }
       
            return NoContent();
        }




    }
}
