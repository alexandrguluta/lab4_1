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
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        .OrderBy(e => e.Name);
        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) =>FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id),trackChanges).SingleOrDefault();

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

        object IEmployeeRepository.GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
           
                employee.CompanyId = companyId;
                Create(employee);
            
        }
    }
}
