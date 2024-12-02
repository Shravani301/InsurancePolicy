using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentController : ControllerBase
    {
        private readonly IInstallmentService _installmentService;

        public InstallmentController(IInstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpPost]
        public IActionResult AddInstallment(InstallmentRequestDto installmentDto)
        {
            var installmentId = _installmentService.AddInstallment(installmentDto);
            return Ok(new { InstallmentId = installmentId });
        }

        [HttpPut]
        public IActionResult UpdateInstallment(InstallmentRequestDto installmentDto)
        {
            _installmentService.UpdateInstallment(installmentDto);
            return Ok("Installment updated successfully.");
        }

        [HttpGet("{Id}")]
        public IActionResult GetInstallmentById(Guid installmentId)
        {
            var installment = _installmentService.GetInstallmentById(installmentId);
            return Ok(installment);
        }

        [HttpGet("policy/{Id}")]
        public IActionResult GetAllInstallmentsForPolicy(Guid policyId)
        {
            var installments = _installmentService.GetAllInstallmentsForPolicy(policyId);
            return Ok(installments);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteInstallment(Guid installmentId)
        {
            _installmentService.DeleteInstallment(installmentId);
            return Ok("Installment deleted successfully.");
        }
    }
}
