namespace employee_management.models
{
    public class LeaveRequest { 
    
        public int leaveId {  get; set; }
        public int employeeId {  get; set; }
        public int managerId {  get; set; }
        public DateTime date {  get; set; }
        public int numberOfDays {  get; set; }
        public string state {  get; set; }
        public LeaveRequest()
        {
            state = "Pending";
        }
    }
}
