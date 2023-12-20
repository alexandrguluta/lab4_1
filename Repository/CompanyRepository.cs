using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Contracts.Contracts;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities.RequestFeatures;

namespace Repository
{
  
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public Company GetCompany(Guid companyId, bool trackChanges) => FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)=> await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>await FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();
        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void AnyMethodFromCompanyRepository()
        {
           
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
                FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public void CreateCompany(Company company) => Create(company);

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
    }


    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId,
 EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId) &&
           (e.Age
            >= employeeParameters.MinAge && e.Age <= employeeParameters.MaxAge),
           trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
            
 return PagedList<Employee>
 .ToPagedList(employees, employeeParameters.PageNumber,
 employeeParameters.PageSize);
        }
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        .OrderBy(e => e.Name);
        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id),trackChanges).SingleOrDefault();

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void AnyMethodFromEmployeeRepository()
        {

        }
        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId,EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId),
            trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
            return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber,
            employeeParameters.PageSize);
        }
        object IEmployeeRepository.GetEmployeesAsync(Guid companyId, Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
           
                employee.CompanyId = companyId;
                Create(employee);
            
        }


        public Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Employee>> IEmployeeRepository.GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
