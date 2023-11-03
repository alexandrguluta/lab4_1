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
        void Save();
    }



namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
namespace Contracts
{
    public interface IEmployeeRepository
    {
        object GetEmployee(Guid companyId, Guid id, bool trackChanges);
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
    }
}
