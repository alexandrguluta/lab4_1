using Contracts;
using Entities;
using Entities.Models;
using static Contracts.Contracts;



namespace Contracts
{
    
    public class Contracts
    {


    }


}
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);

    }
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        Task SaveAsync();
}



namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCompany(Company company);
    }
}
namespace Contracts
{
    public interface IEmployeeRepository
    {

        object GetEmployee(Guid companyId, Guid id, bool trackChanges);
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
