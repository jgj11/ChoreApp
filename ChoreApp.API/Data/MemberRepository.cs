using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoreApp.API.Helpers;
using ChoreApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreApp.API.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DataContext _context;
        public MemberRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Network> GetNetwork(int userId, int recipientId)
        {
            return await _context.Networks.FirstOrDefaultAsync(u => u.NetworkerId == userId && u.NetworkeeId == recipientId);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);
            
            // this is where we are filtering by years of exp
            if (userParams.MinYoe != 0 || userParams.MaxYoe != 99)
            {
                var minYoe = DateTime.Today.AddYears(-userParams.MaxYoe - 1);
                var maxYoe = DateTime.Today.AddYears(-userParams.MinYoe);
                users = users.Where(u => u.DateOfBirth >= minYoe && u.DateOfBirth <= maxYoe);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                        default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            if(userParams.Networkers)
            {
                var userNetworkers = await GetUserNetworks(userParams.UserId, userParams.Networkers);
                users = users.Where(u => userNetworkers.Contains(u.Id));
            }
            if (userParams.Networkees)
            {
                var userNetworkees = await GetUserNetworks(userParams.UserId, userParams.Networkers);
                users = users.Where(u => userNetworkees.Contains(u.Id));

            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserNetworks(int id, bool networkers)
        {
            var user = await _context.Users.Include(x => x.Networkers).Include(x => x.Networkees).FirstOrDefaultAsync(u => u.Id == id);
            if (networkers)
            {
                return user.Networkers.Where(u => u.NetworkeeId == id).Select(i => i.NetworkerId);
            }
            else
            {
                return user.Networkees.Where(u => u.NetworkerId == id).Select(i => i.NetworkeeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}