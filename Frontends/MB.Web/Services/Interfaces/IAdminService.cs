using MB.Web.Models;
using static MB.Web.Models.AdminReturnResult;

namespace MB.Web.Services.Interfaces
{
    public interface IAdminService
    {
        Task<decimal> Revenues(TimePeriodEnum period);
        Task<int> Orders(TimePeriodEnum period);
        Task<int> NumberOfMembers();
        Task<int> NumberOfProducts();
        Task<int> NumberOfCategories();
        Task<string> MostPopularProduct(TimePeriodEnum period);
        Task<List<DataPoint>> SalesOfProductsChart();
        Task<List<DataPoint>> NumberOfOrdersChart();
        Task<List<DataPoint>> TotalOrderAmountsChart();
    }
}
