using Employee_CRUD.Model;

namespace Employee_CRUD.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<bool> Add(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(int id);
       
    }
}
