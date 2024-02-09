using employee_management.models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using employee_management.Dto;
using employee_management.Services;
using System;

namespace employee_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataManager _dataManager;
        
        private readonly LeaveService _leaveService;
        public EmployeeController(DataManager dataManager,LeaveService leaveService)
        {
            _dataManager = dataManager;
            _leaveService = leaveService;
            

        }


       

        [HttpGet]
        [Authorize]
        public IActionResult GetAllReporting() {
            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);

            var employees = _dataManager.GetAllReporting(userId);
           

            return Ok(employees);
        
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile() {
            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);

            var emp = _dataManager.GetEmployee(userId);
            var leaves = _leaveService.GetLeaveRequestProfile(userId);
            var profile = new
            {
                employee=emp,
                leaveRequest= leaves
            };
            return Ok(profile); 

        }



        [HttpPost]
        [Authorize(Roles ="manager")]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);


            Employee employee=new Employee();
            employee.email=employeeDto.email;
            employee.password=employeeDto.password;
            employee.name=employeeDto.name;
            employee.role = "employee";
            
            employee.reportingTo = userId;


            _dataManager.AddEmployee(employee);

            return Ok(employee);
        }

        [HttpPost("Manager")]
        [Authorize(Roles = "admin")]
        public IActionResult AddManager(EmployeeDto employeeDto)
        {
            Employee employee = new Employee();
            employee.email = employeeDto.email;
            employee.password=employeeDto.password;
            employee.name = employeeDto.name;
            employee.role = "manager";
            employee.reportingTo = 1;
            



            _dataManager.AddEmployee(employee);
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id) {
        
            var emp = _dataManager.GetEmployee(id);
            return Ok(emp);
        }


     
    }
}
