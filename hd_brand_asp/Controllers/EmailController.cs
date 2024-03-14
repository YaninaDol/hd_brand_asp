using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace hd_brand_asp.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly Service _emailService;

        public EmailController(ILogger<EmailController> logger, Service emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet("sendDefaultEmail")]
        public IActionResult SendDefaultEmail()
        {
            try
            {
                _emailService.SendEmailDefault();
                return Ok("Сообщение отправлено успешно!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return StatusCode(500, "Ошибка при отправке сообщения");
            }
        }

        [HttpGet("sendCustomEmail")]
        public IActionResult SendCustomEmail([FromQuery] string mail)
        {
            try
            {
                _emailService.SendEmailCustom(mail);
                return Ok("Сообщение отправлено успешно!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return StatusCode(500, "Ошибка при отправке сообщения");
            }
        }
    }

}
