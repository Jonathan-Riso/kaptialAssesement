using Azure;
using kaptialAssesement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Net;

namespace kaptialAssesement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly KapitalDBContext _context;

        public CustomerService(KapitalDBContext context) { _context = context; }

        public async Task<List<Customer>> GetAllCustomers()
        {
           var customers = await _context.Customers.ToListAsync();
            foreach (var customer in customers) {
                if (customer == null) continue;

                var category = _context.CustomerCategories.Find(customer.CategoryId);
            }

           return customers;
        }

        public async Task<Customer> GetCustomerById(int? Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            return customer;
        }

        public async Task<HttpStatusCode> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return HttpStatusCode.Accepted;
        }

        public async Task<HttpStatusCode> EditCustomer(int? Id, Customer customer)
        {
            var found_customer = await _context.Customers.FindAsync(Id);
            if (found_customer == null) { return HttpStatusCode.NotFound; }

			found_customer.PhoneNumber = customer.PhoneNumber;
			found_customer.Name = customer.Name;
			found_customer.Email = customer.Email;
			found_customer.CategoryId = customer.CategoryId;

			await _context.SaveChangesAsync();

            return HttpStatusCode.Accepted;
        }

        public async Task<HttpStatusCode> DeleteCustomer(int Id)
        {
            var customer = _context.Customers.Find(Id);
            if (customer == null) { return HttpStatusCode.NotFound; }
            
            _context.Customers.Remove(customer); 
            await _context.SaveChangesAsync();

            return HttpStatusCode.Accepted;
        }
    }
}
