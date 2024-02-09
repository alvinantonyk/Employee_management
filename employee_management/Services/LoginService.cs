using employee_management.models;

namespace employee_management.Services
{
    public class LoginService
    {
        private readonly DataManager _dataManager;
        public LoginService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Employee ValidLogin(int id, string password)
        {
            var emp = _dataManager.GetEmployee(id);
            if(emp!=null & emp.password==password)
                return emp;
            return null;

        }
    }
}
