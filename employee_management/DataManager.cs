using employee_management.models;

namespace employee_management
{
    public class DataManager
    {
        private List<Employee> Employees { get; set; }
        private List<LeaveRequest> LeaveRequests { get; set; }
        private int userIdCounter { get; set; }
        private int leaveIdCounter {  get; set; }

        public DataManager() { 
            Employees = new List<Employee>();
            LeaveRequests = new List<LeaveRequest>();
            Employees.Add(new Employee { userId=1,name="alvin",password="weekend",role="admin",email="alvin"});
            userIdCounter = 2;
            leaveIdCounter = 1;
            
        }

       

        public void AddEmployee(Employee employee)
        {

                employee.userId = userIdCounter++;
               Employees.Add(employee);
            
        }
        public int Current_UserId()
        {
            return userIdCounter;
        }

        public IEnumerable<Employee> GetEmployees() { 
            return Employees;
        
        }

        public Employee GetEmployee(int id)
        {
            return Employees.FirstOrDefault(x=>x.userId==id);
        }

        public IEnumerable<Employee> GetAllReporting(int id)
        {
            return Employees.Where(x => x.reportingTo == id);
        }

      
        public void ApplyLeave(LeaveRequest leaveRequest)
        {
            leaveRequest.leaveId = leaveIdCounter++;
            LeaveRequests.Add(leaveRequest);

        }

        public IEnumerable<LeaveRequest> GetLeaves()
        {

            return LeaveRequests;
        }

        public LeaveRequest GetLeaveRequest(int id)
        {
            return LeaveRequests.FirstOrDefault(x=>x.leaveId==id);
        }

        public void ApproveLeave(int leaveId)
        {
            var leave = LeaveRequests.FirstOrDefault(x => x.leaveId == leaveId);
            leave.state = "approved";

        }
    }
}
