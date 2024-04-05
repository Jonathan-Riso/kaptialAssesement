using Azure;
using kaptialAssesement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
			if (_isNotValidCustomer(customer)) { return HttpStatusCode.BadRequest; }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        public async Task<HttpStatusCode> EditCustomer(int? Id, Customer customer)
        {
            var found_customer = await _context.Customers.FindAsync(Id);
            if (found_customer == null) { return HttpStatusCode.NotFound; }

			if (_isNotValidCustomer(customer)) { return HttpStatusCode.BadRequest; }

			found_customer.PhoneNumber = customer.PhoneNumber;
			found_customer.Name = customer.Name;
			found_customer.Email = customer.Email;
			found_customer.CategoryId = customer.CategoryId;

			await _context.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        public async Task<HttpStatusCode> DeleteCustomer(int Id)
        {
            var customer = _context.Customers.Find(Id);
            if (customer == null) { return HttpStatusCode.NotFound; }
            
            _context.Customers.Remove(customer); 
            await _context.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        private bool _isNotValidCustomer(Customer customer)
        {
            if (customer == null) return true;
            if (customer.PhoneNumber == null || customer.PhoneNumber.Length != 12)
                return true;
			if (customer.Name == null || customer.Name.Length <= 0 || customer.Name.Length >= 200)
				return true;

			if (customer.Email == null || customer.Email.Length <= 0 || customer.Email.Length >= 200
                || !MailAddress.TryCreate(customer.Email, out var address))
                return true;

			return false;

		}
    }
}
