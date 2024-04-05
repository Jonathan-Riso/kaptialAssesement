using kaptialAssesement.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace kaptialAssesement.Services
{
    public class CustomerCategoryService : ICustomerCategoryService
    {
        private readonly KapitalDBContext _context;

        public CustomerCategoryService(KapitalDBContext context) { _context = context; }

        public async Task<List<CustomerCategory>> GetAllCustomerCategories()
        {
            var categories = await _context.CustomerCategories.ToListAsync();
            return categories;
        }

        public async Task<CustomerCategory> GetCategoryById(int? Id)
        {
            var category = await _context.CustomerCategories.FindAsync(Id);
            return category;
        }

        public async Task<HttpStatusCode> CreateCategory(CustomerCategory category)
		{
            if (category.CategoryName == null || category.CategoryName.Length <= 0  || category.CategoryName.Length > 199) {  return HttpStatusCode.BadRequest; }

			_context.CustomerCategories.Add(category);
			await _context.SaveChangesAsync();

			return HttpStatusCode.Created;
		}

        public async Task<HttpStatusCode> EditCategory(int? Id, CustomerCategory category)
        {
            var found_category = _context.CustomerCategories.Find(Id);
            if (found_category == null) { return HttpStatusCode.NotFound; }

			if (category.CategoryName == null || category.CategoryName.Length <= 0 || category.CategoryName.Length > 199) { return HttpStatusCode.BadRequest; }

            found_category.CategoryName = category.CategoryName;
			await _context.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        public async Task<HttpStatusCode> DeleteCategory(int Id)
        {
            var category = await _context.CustomerCategories.FindAsync(Id);
            if (category == null) { return HttpStatusCode.NotFound; }

            _context.CustomerCategories.Remove(category);
            await _context.SaveChangesAsync();

            return HttpStatusCode.Accepted;
        }
    }
}
