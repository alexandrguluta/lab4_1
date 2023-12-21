using API_Solution.ActionFilters;
using API_Solution.ModelBinders;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Solution.Controllers
{
    [Route("api/drivers/{driverId}/cars")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<UserDto> _dataShaper;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<UserDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersWithHelpAdmin(Guid driverId, [FromQuery] UserParameters carParameters)
        {
            var driver = await _repository.Admin.GetAdminAsync(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Admin with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carsFromDB = await _repository.User.GetUsersAsync(driverId, carParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(carsFromDB.MetaData));
            var carsDto = _mapper.Map<IEnumerable<UserDto>>(carsFromDB);
            return Ok(_dataShaper.ShapeData(carsDto, carParameters.Fields));
        }

        [HttpGet("{id}", Name = "GetUserForAdmin")]
        public async Task<ActionResult> GetUserWithHelpAdmin(Guid driverId, Guid id)
        {
            var driver = await _repository.Admin.GetAdminAsync(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Admin with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carDB = await _repository.User.GetUserByIdAsync(driverId, id, trackChanges: false);
            if(carDB == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var carDto = _mapper.Map<UserDto>(carDB);
            return Ok(carDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUserForAdminAsync(Guid driverId, [FromBody] UserForCreationDto car)
        {           
            var driver = await _repository.Admin.GetAdminAsync(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Admin with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carEntity = _mapper.Map<User>(car);
            _repository.User.CreateUserForAdmin(driverId,carEntity);
            await _repository.SaveAsync();
            var carToReturn = _mapper.Map<UserDto>(carEntity);
            return CreatedAtRoute("GetUserForAdmin", new { driverId, id = carToReturn.Id }, carToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateUserForAdminExistsAttribute))]
        public async Task<IActionResult> DeleteUserForAdmin(Guid driverId, Guid id) 
        {
            var carForAdmin = HttpContext.Items["car"] as User;            
            _repository.User.DeleteUser(carForAdmin);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserForAdminExistsAttribute))]
        public async Task<IActionResult> UpdateUserForAdmin(Guid driverId, Guid id, [FromBody] UserForUpdateDto car)
        {   
            var carEntity = HttpContext.Items["car"] as User;            
            _mapper.Map(car, carEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateUserForAdminExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateUserForAdmin(Guid driverId, Guid id, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }           
            var carEntity = HttpContext.Items["car"] as User;            
            var carToPatch = _mapper.Map<UserForUpdateDto>(carEntity);
            patchDoc.ApplyTo(carToPatch, ModelState);
            TryValidateModel(carToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(carToPatch, carEntity);
            await _repository.SaveAsync();
            return NoContent(); 
        }
    }
}
