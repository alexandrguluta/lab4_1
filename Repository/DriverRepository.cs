using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AdminRepository: RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Admin> GetAdminAsync(Guid id, bool trackChanges) => await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateAdmin(Admin driver) => Create(driver);
        public async Task<IEnumerable<Admin>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteAdmin(Admin driver) => Delete(driver);
    }
}
