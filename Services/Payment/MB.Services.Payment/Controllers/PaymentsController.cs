using MassTransit;
using MB.Services.Payment.Models;
using MB.Shared.ControllerBases;
using MB.Shared.Dtos;
using MB.Shared.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace MB.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            await sendEndpoint.Send<CreateOrderMessageCommand>(new
            {
                paymentDto.Order.BuyerId,
                paymentDto.Order.Address.Province,
                paymentDto.Order.Address.District,
                paymentDto.Order.Address.Street,
                paymentDto.Order.Address.Line,
                paymentDto.Order.Address.ZipCode,
                paymentDto.Order.OrderItems
            });

            return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
