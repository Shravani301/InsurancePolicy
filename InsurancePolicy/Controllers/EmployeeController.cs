using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var employees = _service.GetAll();
        return Ok(employees);
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
        var newEmployeeId = _service.Add(employeeRequestDto);
        return Ok(new { EmployeeId = newEmployeeId, Message = "Employee added successfully" });
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
