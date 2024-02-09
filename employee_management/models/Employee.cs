namespace employee_management.models
{
    public class Employee
    {

       
        public int userId {  get; set; }
        public string password {  get; set; }
        public string name{ get; set; }
        public string email { get; set; }
        public string role{ get; set; }
        public int reportingTo {  get; set; }
    }
}
