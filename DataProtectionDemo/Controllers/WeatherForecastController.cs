using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectionDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IDataProtector _dataProtectionProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _dataProtectionProvider = dataProtectionProvider.CreateProtector("asyaisuiausiuaisuiausiuais");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(string value)
        {
            var encrypted = _dataProtectionProvider.Protect(value);
            var decrypted = _dataProtectionProvider.Unprotect(encrypted);
            //var valueDe = _dataProtectionProvider.Unprotect("CfDJ8ErMC7Mhyu9IhRMQCYSOH5TnGL-XpfxQ_dqCmRd3DVq5q2GoLMPLcBOwXF244A0b_aJulwmf708KFAFhiwZyUPaaaOZZ62h2Pym7EKV9xXSxklyIjzgRolu1Cywf4orT2g");
            return Ok(new {value, encrypted, decrypted});
        }
        [HttpGet("decrypt")]
        public IActionResult Decrypt(string value)
        {
            
            var decrypted = _dataProtectionProvider.Unprotect(value);
            //var valueDe = _dataProtectionProvider.Unprotect("CfDJ8ErMC7Mhyu9IhRMQCYSOH5TnGL-XpfxQ_dqCmRd3DVq5q2GoLMPLcBOwXF244A0b_aJulwmf708KFAFhiwZyUPaaaOZZ62h2Pym7EKV9xXSxklyIjzgRolu1Cywf4orT2g");
            return Ok(new { decrypted });
        }
    }
}