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
        void AnyMethodFromCompanyRepository();
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}
namespace Contracts
{
    public interface IEmployeeRepository
    {
        void AnyMethodFromEmployeeRepository();
    }
}