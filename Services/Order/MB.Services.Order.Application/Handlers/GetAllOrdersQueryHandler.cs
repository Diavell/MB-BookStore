using MB.Services.Order.Application.Dtos;
using MB.Services.Order.Application.Mapping;
using MB.Services.Order.Application.Queries;
using MB.Services.Order.Infrastructure;
using MB.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MB.Services.Order.Application.Handlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetAllOrdersQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
