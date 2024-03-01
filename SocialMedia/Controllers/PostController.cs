using Application.DTOs.PostDto;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _repository;
        private readonly IValidator<Post> _validator;
        private readonly IUserRepository _userRepository;

        public PostController(IMapper mapper, IPostRepository repository, IValidator<Post> validator, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
            _userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _repository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PostGetDto>>(posts));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _repository.GetByIdAsync(id);

            if (post is null)
                return NotFound($"{id} post not found!");

            return Ok(_mapper.Map<PostGetDto>(post));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = _mapper.Map<Post>(dto);
            post.CreatedDateTime = DateTime.UtcNow;

            var validRes = _validator.Validate(post);

            if (!validRes.IsValid)
                return BadRequest(validRes.Errors);

            if(await _userRepository.GetByIdAsync(post.UserId) is null)
                return BadRequest($"{post.UserId} User not found");

            var res = await _repository.CreateAsync(post);

            if(res is null)
                return BadRequest("Create operation failed");

            return Ok(_mapper.Map<PostGetDto>(res));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePost([FromBody] PostUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Post post = await _repository.GetByIdAsync(dto.Id);

            if (post is null)
                return NotFound($"{dto.Id} Post not found");

            _mapper.Map<PostUpdateDto,Post>(dto,post);

            var validRes = _validator.Validate(post);

            if (!validRes.IsValid)
                return BadRequest(validRes.Errors);

            var res = await _repository.UpdateAsync(post);

            if (res is null)
                return BadRequest("Update operation failed");

            return Ok(_mapper.Map<PostGetDto>(res));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePost([FromQuery]Guid id)
        {
            var res = await _repository.DeleteAsync(id);

            if (res)
                return Ok("Deleted successfully");

            return BadRequest("Delete failed");
        }

    }
}
