using Employee_CRUD.Data;
using Employee_CRUD.Migrations;
using Employee_CRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Employee employee)
        {
            _context.Employees.Add(employee);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id); ;
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);

            if (await _context.SaveChangesAsync()> 0)
            {
                return true;
            }
            return false;
        }
        //public async Task Delete(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee != null)
        //    {
        //        _context.Employees.Remove(employee);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<bool> Update(Employee employeeReq)
        {
            var employee = await _context.Employees.FindAsync(employeeReq.Id);
            if (employee == null)
            {
                return false;
            }

            employee.FirstName = employeeReq.FirstName;
            employee.LastName = employeeReq.LastName;
            employee.MiddleName = employeeReq.MiddleName;

            _context.Entry(employee).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

           // _context.Entry(employee).State = EntityState.Modified;
           //await _context.SaveChangesAsync();
            
        }

    }
}
