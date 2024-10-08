﻿using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using BloggingPlatformApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatformApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TagController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _repository;

        public TagController(IMapper mapper, ITagRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<TagDto>))]

        public IActionResult GetTags()
        {
            var tags = _mapper.Map<ICollection<TagDto>>(_repository.GetTags());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        [HttpGet("{TagId}")]
        [ProducesResponseType(200, Type = typeof(TagDto))]
        [ProducesResponseType(400)]
        public IActionResult GetArticle(int TagId)
        {
            if (!_repository.TagExist(TagId))
            {
                return NotFound();
            }

            var Tag = _mapper.Map<TagDto>(_repository.GetTag(TagId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Tag);
        }

        [HttpGet("article/{TagId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ArticleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetArticleByTag(int TagId)
        {
            if (!_repository.TagExist(TagId))
            {
                return NotFound();
            }
            var articles = _mapper.Map<ICollection<ArticleDto>>(_repository.GetArticleByTag(TagId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(articles);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTag(int ArticleId, [FromBody] TagDto tag)
        {
            if (tag == null)
            {
                return BadRequest(ModelState);
            }
            var tags = _repository.GetTags().FirstOrDefault(t => t.Name.Trim().ToUpper() == tag.Name.Trim().ToUpper());
            if (tags != null)
            {
                ModelState.AddModelError("", "Tag Already Exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var TagMap = _mapper.Map<Tag>(tag);
            if (!_repository.CreateTag(ArticleId, TagMap))
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
        public IActionResult UpdateTag(TagDto tag)
        {
            if (tag == null)
            {
                return BadRequest(ModelState);
            }
            var UpdatedTag= _mapper.Map<Tag>(tag);
            if (!_repository.UpdateTag(UpdatedTag))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{TagId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTag(int TagId)
        {
            if (!_repository.TagExist(TagId))
            {
                return NotFound();
            }
            var tag = _repository.GetTag(TagId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.DeleteTag(tag))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
