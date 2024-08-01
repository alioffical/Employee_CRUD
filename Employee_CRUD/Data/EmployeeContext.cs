using Employee_CRUD.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Employee_CRUD.Data
{
        public class EmployeeContext : DbContext
        {
            public EmployeeContext(DbContextOptions<EmployeeContext> options)
                : base(options)
            {
            }

            public DbSet<Employee> Employees { get; set; }
        
        }
}
