using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Collections.Specialized.BitVector32;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CompaniesController(IRepositoryManager repository, ILoggerManager
   logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _repository.Company.GetAllCompanies(trackChanges: false);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return Ok(companiesDto);
            
    }
    
    [HttpGet("api/companies/{id}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _repository.Company.GetCompany(id, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        else
        {
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }
    }
    
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IRepositoryManager repository, ILoggerManager
       logger,
        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
    }
    [HttpGet("{id}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
        return NotFound();
        }
        var employeeDb = _repository.Employee.GetEmployee(companyId, id,
       trackChanges:
        false);
        if (employeeDb == null)
        {
            _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
        return NotFound();
        }
        var employee = _mapper.Map<EmployeeDto>(employeeDb);
        return Ok(employee);
    }
    [HttpPost("/employees")]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
    {
        if (company == null)
        {
            _logger.LogError("CompanyForCreationDto object sent from client is null.");
        return BadRequest("CompanyForCreationDto object is null");
        }
        var companyEntity = _mapper.Map<Company>(company);
        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();
        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
        companyToReturn);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody]
        EmployeeForCreationDto employee)
    {
        if (employee == null)
        {
            _logger.LogError("EmployeeForCreationDto object sent from client is null.");
        return BadRequest("EmployeeForCreationDto object is null");
        }
        var company = _repository.Company.GetCompany(companyId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
        return NotFound();
        }
        var employeeEntity = _mapper.Map<Employee>(employee);
        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();
        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return CreatedAtRoute("GetEmployeeForCompany", new
        {
            companyId,
            id = employeeToReturn.Id
        }, employeeToReturn);

    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        if (ids == null)
        {
            _logger.LogError("Parameter ids is null");
            return BadRequest("Parameter ids is null");
        }
        var companyEntities = _repository.Company.GetByIds(ids, trackChanges: false);
        
        if (ids.Count() != companyEntities.Count())
        {
            _logger.LogError("Some ids are not valid in a collection");
            return NotFound();
        }
        var companiesToReturn =
       _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return Ok(companiesToReturn);
    }
    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody]
    IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection == null)
        {
            _logger.LogError("Company collection sent from client is null.");
            return BadRequest("Company collection is null");
        }
        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach (var company in companyEntities)
        {
            _repository.Company.CreateCompany(company);
        }
        _repository.Save();
        var companyCollectionToReturn =
        _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
        return CreatedAtRoute("CompanyCollection", new { ids },
        companyCollectionToReturn);
    }
}
