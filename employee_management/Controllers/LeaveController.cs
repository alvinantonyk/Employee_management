using employee_management.Dto;
using employee_management.models;
using employee_management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employee_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {

        private readonly LeaveService _leaveService;

        public LeaveController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult ApplyLeave(LeaveRequestDto leaveRequestDto)
        {
            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);




            LeaveRequest request = new LeaveRequest { date = leaveRequestDto.date, numberOfDays = leaveRequestDto.numberOfDays };
            _leaveService.ApplyLeave(request, userId);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult getLeaves()
        {
            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);
            var leaves = _leaveService.GetLeaveRequests(userId);

            return Ok(leaves);
        }

        [HttpPut]
        [Authorize(Roles = "manager")]
        public IActionResult ApproveLeave(int LeaveId)
        {

            var userIdClaim = User.FindFirst("userid");
            var userId = int.Parse(userIdClaim.Value);

            if (_leaveService.ApproveLeave(userId, LeaveId))
                return Ok();

            return BadRequest();


        }
    }
}
