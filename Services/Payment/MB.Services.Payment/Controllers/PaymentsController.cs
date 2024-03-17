using MB.Shared.ControllerBases;
using MB.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MB.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
