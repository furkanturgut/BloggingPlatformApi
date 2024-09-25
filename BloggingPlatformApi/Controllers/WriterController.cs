using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatformApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WriterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWriterRepository _repository;

        public WriterController(IMapper mapper, IWriterRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<WriterDto>))]
        public ActionResult GetWriters()
        {
            var writers = _mapper.Map<ICollection<WriterDto>>(_repository.GetWriters());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(writers);
        }

        [HttpGet("{WriterId}")]
        [ProducesResponseType(200, Type = typeof(WriterDto))]
        [ProducesResponseType(400)]
        public ActionResult GetWriter(int WriterId)
        {
            if (!_repository.WriterExists(WriterId))
            {
                return NotFound();
            }
            var writer = _mapper.Map<WriterDto>(_repository.GetWriter(WriterId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(writer);
        }

        [HttpGet("articles/{WriterId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ArticleDto>))]
        [ProducesResponseType(400)]
        public ActionResult GetArticlesByWriter(int WriterId)
        {
            if (!_repository.WriterExists(WriterId))
            {
                return NotFound();
            }
            var articles = _mapper.Map<ICollection<ArticleDto>>(_repository.GetArticleByWriter(WriterId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(articles);
        }
        [HttpGet("{ArticleId}/writer")]
        [ProducesResponseType(200, Type = typeof(WriterDto))]
        public ActionResult GetArticleWriter(int ArticleId)
        {
            var writer = _mapper.Map<WriterDto>(_repository.GetArticleWriter(ArticleId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(writer);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWriter(WriterDto writer)
        {
            if (writer == null)
            {
                return BadRequest(ModelState);
            }
            var writers = _repository.GetWriters().FirstOrDefault(w => w.Surname.Trim().ToUpper() == writer.Surname.Trim().ToUpper());
            if (writers != null)
            {
                ModelState.AddModelError("", "Writer Already Exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WriterMap = _mapper.Map<Writer>(writer);
            if (!_repository.CreateWriter(WriterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly Saved");
        }
    }
}
