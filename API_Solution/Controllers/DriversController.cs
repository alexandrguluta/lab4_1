using API_Solution.ActionFilters;
using API_Solution.ModelBinders;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public AdminsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var drivers = await _repository.Admin.GetAllAdminsAsync(trackChanges: false);
            var driversDto = _mapper.Map<IEnumerable<Admin>>(drivers);
            return Ok(driversDto);
        }

        [HttpGet("{id}", Name = "AdminById")]
        public async Task<IActionResult> GetAdminAsync(Guid id)
        {
            var driver = await _repository.Admin.GetAdminAsync(id, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Admin with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var driverDto = _mapper.Map<AdminDto>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAdminAsync([FromBody] AdminForCreatonDto driver) 
        {            
            var driverEntity = _mapper.Map<Admin>(driver);
            _repository.Admin.CreateAdmin(driverEntity);
            await _repository.SaveAsync();
            var driverToReturn = _mapper.Map<AdminDto>(driverEntity);
            return CreatedAtRoute("AdminById", new { id = driverToReturn.Id }, driverToReturn);
        }

        [HttpGet("collection/({ids})", Name = "AdminCollection")]
        public async Task<IActionResult> GetAdminCollection(IEnumerable<Guid> ids) 
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var driverEntities = await _repository.Admin.GetByIdsAsync(ids, trackChanges: true);
            if (ids.Count() != driverEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var driversToReturn = _mapper.Map<IEnumerable<AdminDto>> (driverEntities);
            return Ok(driversToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateAdminCollection([ModelBinder (BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> driverCollection)
        {
            if (driverCollection == null)
            {
                _logger.LogError("Admin collection sent from client is null.");
                return BadRequest("Admin collection is null");
            }
            var driverEntitiees = _mapper.Map<IEnumerable<Admin>>(driverCollection);
            foreach (var driver in driverEntitiees)
            {
                _repository.Admin.CreateAdmin(driver);
            }
            await _repository.SaveAsync();
            var driverCollectionToReturn = _mapper.Map<IEnumerable<AdminDto>>(driverEntitiees);
            var ids = string.Join(",", driverCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("AdminCollection", new { ids }, driverCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAdminExistsAtribute))]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var driver = HttpContext.Items["driver"] as Admin;
            _repository.Admin.DeleteAdmin(driver);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAdminExistsAtribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] AdminForUpdateDto driver)
        {            
            var driverEntity = HttpContext.Items["driver"] as Admin;
            _mapper.Map(driver, driverEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
