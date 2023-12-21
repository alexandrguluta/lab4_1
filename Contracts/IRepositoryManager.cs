namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IUserRepository User { get; }
        IAdminRepository Admin { get; }
        public Task SaveAsync();
    }
}
