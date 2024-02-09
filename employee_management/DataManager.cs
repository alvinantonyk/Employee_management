using employee_management.models;

namespace employee_management
{
    public class DataManager
    {
        private List<Employee> Employees { get; set; }
        private int userIdCounter { get; set; }

        public DataManager() { 
            Employees = new List<Employee>();
            Employees.Add(new Employee { userId=1,name="alvin",username="alvin",role="admin",email="alvin"});
            userIdCounter = 2;
            
        }

        public void AddEmployee(Employee employee)
        {
            
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

    }
}
