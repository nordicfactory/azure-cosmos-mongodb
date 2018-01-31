using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Test
{
    public interface IApplicationUserRepository
    {
        Task<bool> CreateUserAsync(
            ApplicationUser user);
        Task<bool> DeleteAsync(
            string userId);
        Task<ApplicationUser> FindByIdAsync(
            string userId);
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IMongoCollection<ApplicationUser> _users;

        public ApplicationUserRepository(IDbContext dbContext)
        {
            _users = dbContext.GetCollection<ApplicationUser>("Users");
        }

        public async Task<bool> CreateUserAsync(
            ApplicationUser user)
        {
            try
            {
                await _users.InsertOneAsync(user);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(
            string userId)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.Id, userId);

            var deleteResult = await _users.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
        }

        public async Task<ApplicationUser> FindByIdAsync(
            string userId)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.Id, userId);
            var usersCursor = await _users.FindAsync(filter);
            return await usersCursor.SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateUserAsync(
            ApplicationUser updatedUser)
        {
            try
            {
                var filter = Builders<ApplicationUser>.Filter.Eq(
                    user => user.Id,
                    updatedUser.Id);

                await _users.ReplaceOneAsync(
                    filter,
                    updatedUser);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
