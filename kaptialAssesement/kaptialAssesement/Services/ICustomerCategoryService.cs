using kaptialAssesement.Models;
using System.Net;

namespace kaptialAssesement.Services
{
    public interface ICustomerCategoryService
    {
        Task<List<CustomerCategory>> GetAllCustomerCategories();
        Task<CustomerCategory> GetCategoryById(int? Id);
        Task<HttpStatusCode> CreateCategory(CustomerCategory category);
        Task<HttpStatusCode> EditCategory(int? Id, CustomerCategory category);
        Task<HttpStatusCode> DeleteCategory(int Id);
    }
}
