using employee_management.models;

namespace employee_management.Services
{
    public class LeaveService
    {
        private readonly DataManager _dataManager;

        public LeaveService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void ApplyLeave(LeaveRequest leaveRequest,int userId)
        {
            
            var emp= _dataManager.GetEmployee(userId);

            leaveRequest.employeeId=userId;
            leaveRequest.managerId = emp.reportingTo;
            _dataManager.ApplyLeave(leaveRequest);
        }
        public IEnumerable<LeaveRequest> GetLeaveRequests(int id)
        {
            var emps = _dataManager.GetLeaves();
            return emps.Where(x => x.managerId == id);
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestProfile(int id)
        {
            var emps = _dataManager.GetLeaves();
            return emps.Where(x => x.employeeId == id);
        }

        public bool ApproveLeave(int userId,int LeaveId)
        {
            var leave=_dataManager.GetLeaveRequest(LeaveId);
            if (leave!=null && leave.managerId == userId && leave.state=="Pending")
            {
                _dataManager.ApproveLeave(LeaveId);
                return true;

            }
            
            return false;
           
        }
    }
}
