using CopyChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace CopyChallenge.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Get()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer { ID = 1, CompanyName = "ABC Company", City = "New York", State = "NY", Phone = "123-456-7890", Fax = "987-654-3210" },
                new Customer { ID = 2, CompanyName = "XYZ Inc.", City = "Los Angeles", State = "CA", Phone = "555-555-5555", Fax = "999-999-9999" },
                new Customer { ID = 3, CompanyName = "Company X", City = "Chicago", State = "IL", Phone = "111-222-3333", Fax = "444-555-6666" },
                new Customer { ID = 4, CompanyName = "ABC Corporation", City = "Houston", State = "TX", Phone = "777-888-9999", Fax = "222-333-4444" },
                new Customer { ID = 5, CompanyName = "XYZ Ltd.", City = "San Francisco", State = "CA", Phone = "444-333-2222", Fax = "555-666-7777" },
                new Customer { ID = 6, CompanyName = "Company Y", City = "Boston", State = "MA", Phone = "888-999-0000", Fax = "111-222-3333" },
                new Customer { ID = 7, CompanyName = "ABC Industries", City = "Seattle", State = "WA", Phone = "222-333-4444", Fax = "555-666-7777" },
                new Customer { ID = 8, CompanyName = "XYZ Co.", City = "Atlanta", State = "GA", Phone = "999-888-7777", Fax = "444-333-2222" },
                new Customer { ID = 9, CompanyName = "Company Z", City = "Miami", State = "FL", Phone = "333-444-5555", Fax = "666-777-8888" },
                new Customer { ID = 10, CompanyName = "ABC Ltd.", City = "Denver", State = "CO", Phone = "666-777-8888", Fax = "333-444-5555" },
                new Customer { ID = 11, CompanyName = "XYZ Corporation", City = "Dallas", State = "TX", Phone = "888-999-0000", Fax = "111-222-3333" },
                new Customer { ID = 12, CompanyName = "Company A", City = "Phoenix", State = "AZ", Phone = "111-222-3333", Fax = "444-555-6666" },
                new Customer { ID = 13, CompanyName = "ABC Co.", City = "Philadelphia", State = "PA", Phone = "444-555-6666", Fax = "111-222-3333" },
                new Customer { ID = 14, CompanyName = "XYZ Industries", City = "San Diego", State = "CA", Phone = "222-333-4444", Fax = "555-666-7777" },
                new Customer { ID = 15, CompanyName = "Company B", City = "Portland", State = "OR", Phone = "555-666-7777", Fax = "222-333-4444" },
                new Customer { ID = 16, CompanyName = "ABC Corporation", City = "Detroit", State = "MI", Phone = "777-888-9999", Fax = "222-333-4444" },
                new Customer { ID = 17, CompanyName = "XYZ Company", City = "Las Vegas", State = "NV", Phone = "444-333-2222", Fax = "555-666-7777" },
                new Customer { ID = 18, CompanyName = "Company C", City = "Washington", State = "DC", Phone = "888-999-0000", Fax = "111-222-3333" },
                new Customer { ID = 19, CompanyName = "ABC Ltd.", City = "Minneapolis", State = "MN", Phone = "222-333-4444", Fax = "555-666-7777" },
                new Customer { ID = 20, CompanyName = "XYZ Inc.", City = "Orlando", State = "FL", Phone = "999-888-7777", Fax = "444-333-2222" }

            };

            return Json(customers);
        }
    }
}
