﻿using MB.Services.Order.Application.Commands;
using MB.Services.Order.Application.Queries;
using MB.Shared.ControllerBases;
using MB.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MB.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        private readonly ISharedIdentityService _sharedIdentityService; 

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _mediator.Send(new GetAllOrdersQuery());
            return CreateActionResultInstance(response);
        }
    }
}
