using MB.Web.Models;

namespace MB.Web.Services.Interfaces
{
    public interface IAdminService
    {
        Task<decimal> MonthltyEarning();
        Task<decimal> AnnualEarning();
        Task<int> MonthltyOrders();
        Task<int> AnnualOrders();
        Task<int> NumberOfMembers();
        Task<int> NumberOfProducts();
        Task<int> NumberOfCategories();
        Task<List<DataPoint>> SalesOfProductsChart();
        Task<List<DataPoint>> NumberOfOrdersChart();
        Task<List<DataPoint>> TotalOrderAmountsChart();
    }
}
