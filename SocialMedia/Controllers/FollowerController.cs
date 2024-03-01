using Application.DTOs.FollowerDto;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FollowerController(IFollowerRepository followerRepository, IMapper mapper, IUserRepository userRepository)
        {
            _followerRepository = followerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllFollowers()
        {
            var followers = await _followerRepository.GetAllAsync();

            if (followers is null)
                return Ok(followers);

            return Ok(_mapper.Map<IEnumerable<FollowerDto>>(followers));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllFollowerById(Guid id)
        {
            var followers = await _followerRepository.GetByIdAsync(id);

            if (followers is null)
                return NotFound($"{id} not found!");

            return Ok(_mapper.Map<FollowerDto>(followers));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllFollowerByFollowerId(Guid id)
        {
            var followers = (await _followerRepository.GetAllAsync())
                .Where(x => x.FollowerUserId == id);

            if (followers is null)
                return NotFound($"FollowerId : {id} not found!");

            return Ok(_mapper.Map<IEnumerable<FollowerDto>>(followers));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllFollowerByFollowedId(Guid id)
        {
            var followed = (await _followerRepository.GetAllAsync())
                .Where(x => x.FollowedUserId == id);

            if (followed is null)
                return NotFound($"Followed : {id} not found!");

            return Ok(_mapper.Map<IEnumerable<FollowerDto>>(followed));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostFollower([FromBody]FollowerCreateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userRepository.GetByIdAsync(dto.FollowerUserId) is null)
                return NotFound($"FollowerId : {dto.FollowerUserId} not found!");

            if(await _userRepository.GetByIdAsync(dto.FollowedUserId) is null)
                return NotFound($"FolloweDId : {dto.FollowedUserId} not found!");

            var follower = _mapper.Map<FollowerCreateDto,Follower>(dto);

            var res = await _followerRepository.CreateAsync(follower);
            
            if(res is null)
                 return BadRequest("Create failed");

            return Ok(res);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFollowerById([FromQuery]Guid id)
        {
            var res = await _followerRepository.DeleteAsync(id);
            
            if(res)
                return Ok("Deleted successfully");

            return BadRequest("Delete failed");
        }
    }
}
