using kaptialAssesement.Models;
using System.Net;

namespace kaptialAssesement.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int? Id);
        Task<HttpStatusCode> CreateCustomer(Customer customer);
        Task<HttpStatusCode> EditCustomer(int? Id, Customer customer);
        Task<HttpStatusCode> DeleteCustomer(int Id);
    }
}
