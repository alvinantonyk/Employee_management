using employee_management.models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using employee_management.Dto;

namespace employee_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataManager _dataManager;
        public EmployeeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        [HttpPost("user")]
        public async Task<IActionResult> login( int userid)
        {
            var emp=_dataManager.GetEmployee(userid);
            if(emp==null)
                return Unauthorized();
            var username = emp.username;
            var role = emp.role;
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role,role)

            };
            var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            Console.WriteLine("added user");
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll() {

            var a= _dataManager.GetEmployees().ToArray();
            return Ok(a);
        
        }


        [HttpPost]
        [Authorize(Roles ="manager")]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee=new Employee();
            employee.email=employeeDto.email;
            employee.username=employeeDto.username;
            employee.name=employeeDto.name;
            employee.role = "employee";
            employee.userId = _dataManager.Current_UserId();
            _dataManager.AddEmployee(employee);
            return Ok(employee);
        }

        [HttpPost("Manager")]
        [Authorize(Roles = "admin")]
        public IActionResult AddManager(EmployeeDto employeeDto)
        {
            Employee employee = new Employee();
            employee.email = employeeDto.email;
            employee.username = employeeDto.username;
            employee.name = employeeDto.name;
            employee.role = "manager";
            employee.userId = _dataManager.Current_UserId();
            _dataManager.AddEmployee(employee);
            return Ok(employee);
        }
    }
}
