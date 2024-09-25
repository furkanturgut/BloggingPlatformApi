using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatformApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ArticleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _repository;

        public ArticleController(IMapper mapper, IArticleRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
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
        

    }
}
