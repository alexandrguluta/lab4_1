using Entities.Models;

namespace Contracts
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAdminsAsync(bool trackChanges);
        public Task<Admin> GetAdminAsync(Guid id, bool trackChanges);
        void CreateAdmin(Admin driver);       
        Task<IEnumerable<Admin>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteAdmin(Admin driver);
    }
}
