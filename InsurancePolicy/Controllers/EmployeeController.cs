using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            var customers = _service.GetAllPaginated(pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", customers.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", customers.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", customers.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", customers.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", customers.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", customers.HasPrevious.ToString());

            // Return the customers in the response body
            return Ok(customers);
        }
    [HttpPut("activate")]
    public IActionResult Activate(Guid id)
    {
        _service.Activate(id);
        return Ok(id);
    }
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var employee = _service.GetById(id);
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Add(EmployeeRequestDto employeeRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = string.Join("; ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            throw new ValidationException($"{errors}");
        }
        var newEmployeeId = _service.Add(employeeRequestDto);
        return Ok(new { EmployeeId = newEmployeeId, Message = "Employee added successfully" });
        

    }
    [HttpPut("{id}")]
    public IActionResult UpdateSalary(Guid id,double salary)
    {
         _service.UpdateSalary(id,salary);
        return Ok(new {Message = "Employee updated successfully" });
    }

    [HttpPut]
    public IActionResult Modify(EmployeeRequestDto employeeRequestDto)
    {
        _service.Update(employeeRequestDto);
        return Ok(new { Message = "Employee updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _service.Delete(id);
        return Ok(new { Message = "Employee deleted successfully" });
    }
}
