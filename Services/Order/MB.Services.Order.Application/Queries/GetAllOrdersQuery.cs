using MB.Services.Order.Application.Dtos;
using MB.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Services.Order.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<Response<List<OrderDto>>>
    {
    }
}
