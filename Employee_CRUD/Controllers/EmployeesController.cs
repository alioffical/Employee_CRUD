using Employee_CRUD.Model;
using Employee_CRUD.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] Employee employee)
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
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
        public async Task<IActionResult> PutEmployee([FromBody] Employee employee)
        {
            if (employee.Id <= 0)
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                var existingEmployee = await _repository.GetById(employee.Id);
                return Ok(new { Success = existingEmployee, data = employee });
            }
            catch (Exception)
            {

                throw;
            }
           
            //if (existingEmployee == null)
            //{
            //    return NotFound($"Employee with ID {employee.Id} not found.");
            //}

            //try
            //{
            //    await _repository.Update(existingEmployee);
            //}
             
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "An internal server error occurred.");
            //}

           
        }
        

        [HttpPost("DeletebyId/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);
            return Ok("Record Deleted Successfully");
        }

    }
}
