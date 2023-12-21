using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace API_Solution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private Company _company;
        private Employee _employee;

        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _repository.Company.Delete(_company);
            _repository.Employee.Create(_employee);
            return new string[] { "value1", "value2" };
        }
    }
}