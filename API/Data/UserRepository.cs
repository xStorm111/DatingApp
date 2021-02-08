using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            //SELECT QUERY
            // return await _context.Users.Where(user => user.UserName == username).Select(user => new MemberDto{
            //     Id = user.Id,
            //     Username = user.UserName,
            //     blablabla
            // }).SingleOrDefaultAsync();

            return await _context.Users
            .Where(user => user.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider) //get the properties we want directly from query
            .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {

            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDoB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDoB = DateTime.Today.AddYears(-userParams.MinAge - 1);

            query = query.Where(u => u.DateOfBirth >= minDoB && u.DateOfBirth <= maxDoB);
            query = userParams.orderBy switch
            { //since c# v8
                "createdAt" => query.OrderByDescending(u => u.CreatedAt),
                _ => query.OrderByDescending(u => u.LastActive) //_ is default
            };

            //Since we now will only read from this, we don't need to track this on EntityFramework
            return await PagedList<MemberDto>.CreateAsync(
                query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                userParams.PageNumber,
                userParams.pageSize
                );
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
            .Select(x => x.Gender).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(p => p.Photos).ToListAsync();
        }
        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}