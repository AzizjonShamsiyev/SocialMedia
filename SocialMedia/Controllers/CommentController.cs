using Application.DTOs.CommentDto;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Comment> _validator;
        private readonly ICommentRepository _repository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CommentController(IMapper mapper, IValidator<Comment> validator, ICommentRepository repository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllComment()
        {
            var comments = await _repository.GetAllAsync();
            var getDto = _mapper.Map<IEnumerable<CommentGetDto>>(comments);
            return Ok(getDto);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdComment(Guid id)
        {
            Comment comments = await _repository.GetByIdAsync(id);
            var getDto = _mapper.Map<CommentGetDto>(comments);
            return Ok(getDto);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateComment([FromBody]CommentCreateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            Comment comment = _mapper.Map<Comment>(dto);

            if(await _postRepository.GetByIdAsync(comment.PostId) is null)
                return NotFound($"{comment.PostId} Post not found");

            if(await _userRepository.GetByIdAsync(comment.UserId) is null)
                return NotFound($"{comment.UserId} User not found");

            comment.CreatedDateTime = DateTime.UtcNow;

            var validationRes = await _validator.ValidateAsync(comment);
            
            if (!validationRes.IsValid) 
                return BadRequest(validationRes.Errors);

            var res = await _repository.CreateAsync(comment);

            if(res is null)
                return BadRequest("Create operation failed!");

            CommentGetDto getDto = _mapper.Map<CommentGetDto>(res);

            return Ok(getDto);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateComment([FromBody]CommentUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Comment comment = await _repository.GetByIdAsync(updateDto.Id);

            if(comment is null)
                return BadRequest($"CommentId : {updateDto.Id} not found!");

            _mapper.Map<CommentUpdateDto,Comment>(updateDto,comment);

            var validationRes = await _validator.ValidateAsync(comment);

            if (!validationRes.IsValid)
                return BadRequest(validationRes.Errors);

            Comment? res = await _repository.UpdateAsync(comment);

            if (res is null)
                return BadRequest("Create operation failed!");

            CommentGetDto getDto = _mapper.Map<CommentGetDto>(res);

            return Ok(getDto);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteComment([FromQuery]Guid id)
        {
            var res = await _repository.DeleteAsync(id);
            
            if(res)
                return Ok("Delete successfully");

            return BadRequest("Delete failed!");
        }

    }
}
