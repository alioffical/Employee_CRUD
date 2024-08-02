using Employee_CRUD.Model;
using Employee_CRUD.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Employee_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {


            if (string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.MiddleName) || string.IsNullOrWhiteSpace(employee.LastName))
            {
                return BadRequest("Employee's Data required.");
            }

            try
            {
                var result = await _repository.Add(employee);
                return Ok(new { Success = result, data = employee });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred: {ex.Message}");
            }

        }


        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _repository.GetAll();
            return Ok(employees);
        }

        [HttpGet("GetbyId/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _repository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPost("UpdateById")]
        public async Task<IActionResult> UpdateById([FromBody] Employee employee)
        {
            try
            {
                var existingEmployee = await _repository.Update(employee);

                if (existingEmployee)
                {
                    return Ok(new { Success = existingEmployee, data = employee });
                }
                else
                {
                    return Ok(new { Success = false, data = employee });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost("DeletebyId/{id}")]
        public async Task<IActionResult> DeletebyId(int id)
        {
            var employee = await _repository.GetById(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            bool isDeleted = await _repository.Delete(id);
            if (isDeleted)
            {
                return Ok("Record deleted successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while deleting the record.");
            }
        }

    }
}
