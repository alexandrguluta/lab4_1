using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetUsersAsync(Guid driverId, UserParameters carParameters, bool trackChanges);
        Task<User> GetUserByIdAsync(Guid driverId, Guid carId, bool trackChanges);
        void CreateUserForAdmin(Guid driverId, User car);
        void DeleteUser(User car);
    }
}
