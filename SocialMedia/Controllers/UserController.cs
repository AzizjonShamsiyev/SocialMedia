    using Application.DTOs.UserDto;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;

        public UserController(IValidator<User> validator, IUserRepository userRepository, IMapper mapper)
        {
            _validator = validator;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<UserGetDto>> GetAllUsers()
        {
            IQueryable<User> users = await _userRepository.GetAllAsync();

            IEnumerable<UserGetDto> dto = _mapper
                .Map<IEnumerable<UserGetDto>>(users.AsEnumerable());

            return Ok(dto);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<UserGetDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if(user is null)
                return NotFound($"{id} user not found!");

            UserGetDto? dto = _mapper
                .Map<UserGetDto>(user);

            return Ok(dto);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserGetDto>> CreateUser([FromBody]UserCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _mapper
                .Map<UserCreateDto , User>(createDto);

            var validate = await _validator.ValidateAsync(user);

            if (!validate.IsValid)
                return BadRequest(validate.Errors);

            User response =
                await _userRepository.CreateAsync(user);

            if (response is null)
                return BadRequest("Create operation failed");

            UserGetDto dto = _mapper.Map<UserGetDto>(response);

            return Ok(dto);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<UserGetDto>> UpdateUser([FromBody] UserUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _mapper
                .Map<UserUpdateDto,User>(updateDto);

            var validate = await _validator.ValidateAsync(user);

            if (!validate.IsValid)
                return BadRequest(validate.Errors);

            User response =
                await _userRepository.UpdateAsync(user);

            if (response is null)
                return BadRequest("Update operation failed");
            
            UserGetDto dto = _mapper.Map<UserGetDto>(response);

            return Ok(dto);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<UserGetDto>> DeleteUser([FromQuery] Guid id)
        {
            bool response =
                await _userRepository.DeleteAsync(id);

            if (response)
                return Ok("Deleted successfully");

            return BadRequest("Delete operation failed");
        }
    }
}
