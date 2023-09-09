using Microsoft.AspNetCore.Mvc;
using mvcCRUD.Models;
using mvcCRUD.Repository.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;

namespace mvcCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Department> departmentRepository, IGenericRepository<Employee> employeeRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentsList()
        {
            List<Department> list = new List<Department>();
            list = await _departmentRepository.ModelList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesList()
        {
            List<Employee> list = new List<Employee>();
            list = await _employeeRepository.ModelList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee model)
        {
            bool result = await _employeeRepository.NewMemberModel(model);

            if(result)
                return StatusCode(StatusCodes.Status200OK, new { value = result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = result, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] Employee model)
        {
            bool result = await _employeeRepository.EditMemberModel(model);

            if (result)
                return StatusCode(StatusCodes.Status200OK, new { value = result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = result, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int idEmployee)
        {
            bool result = await _employeeRepository.DeleteMemberModel(idEmployee);

            if (result)
                return StatusCode(StatusCodes.Status200OK, new { value = result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = result, msg = "error" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
