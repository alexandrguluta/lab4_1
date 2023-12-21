using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<PagedList<User>> GetUsersAsync(Guid driverId, UserParameters carParameters, bool trackChanges)
        {
            var cars = await FindByCondition(c => c.AdminId.Equals(driverId), trackChanges).Search(carParameters.SearchTerm).Sort(carParameters.OrderBy).ToListAsync();
            return PagedList<User>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }
        public async Task<User> GetUserByIdAsync(Guid driverId, Guid id, bool trackChanges) => await FindByCondition(c => c.AdminId.Equals(driverId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateUserForAdmin(Guid driverId, User car)
        {
            car.AdminId = driverId;
            Create(car);
        }
        public void DeleteUser(User car) => Delete(car);
    }
}
