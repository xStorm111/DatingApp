using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        //IEnumerable is for list/arrays, not single objects
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // var users = await _userRepository.GetUsersAsync();
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            var users = await _userRepository.GetMembersAsync();
            //we need to wrap users with Ok
            return Ok(users);
        }

        // api/users/3
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //in the course Niel says we can reduce this to only return _context.Users.Find(id);
            //i still prefer this way because its a lot easier to debug if we need too

            // var user = await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberDto>(user);
            var user = await _userRepository.GetMemberAsync(username);
            return user;
        }
    }
}
