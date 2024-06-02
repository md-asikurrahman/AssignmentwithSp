using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProjectWithSp.WEB.Data;
using TestProjectWithSp.WEB.Models;

namespace TestProjectWithSp.WEB.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.FromSqlRaw("exec spGetAllCustomer").ToListAsync();
            return View(customers);
        }

      

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerData = $"spInsertCustomer @name ='{customer.Name}',@isActive='{customer.IsActive}',@dob='{customer.Dob}',@mobileNo='{customer.MobileNo}',@Nidno='{customer.NidNo}'";
                _context.Database.ExecuteSqlRaw(customerData);
                
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  _context.Customers.FromSqlRaw($"spGetCustomerbyId '{id}'");
            if (customer == null)
            {
                return NotFound();
            }
            var existingCustomer = new Customer();
            foreach (var item in customer)
            {
                existingCustomer.Id = item.Id;
                existingCustomer.Name = item.Name;
                existingCustomer.MobileNo = item.MobileNo;
                existingCustomer.Dob = item.Dob;
                existingCustomer.NidNo = item.NidNo;
                existingCustomer.IsActive = item.IsActive;
                existingCustomer.CreatedDate = item.CreatedDate;

            }
            return View(existingCustomer);
        }

        // POST: Customers/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dataParam = $"spUpdateCustomer @id={id}, @name ='{customer.Name}',@dob='{customer.Dob}',@mobileNo={customer.MobileNo},@Nidno={customer.NidNo},@createdAt='{customer.CreatedDate}',@isActive={customer.IsActive}";
                    _context.Database.ExecuteSqlRaw(dataParam);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.FromSqlRaw($"spGetCustomerbyId '{id}'");
            if (customer == null)
            {
                return NotFound();
            }
            var existingCustomer = new Customer();
            foreach (var item in customer)
            {
                existingCustomer.Id = item.Id;
                existingCustomer.Name = item.Name;
                existingCustomer.MobileNo = item.MobileNo;
                existingCustomer.Dob = item.Dob;
                existingCustomer.NidNo = item.NidNo;
                existingCustomer.IsActive = item.IsActive;
                existingCustomer.CreatedDate = item.CreatedDate;

            }
            return View(existingCustomer);
        }
        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataParam = $"spDeleteCustomer @id={id}";
            _context.Database.ExecuteSqlRaw(dataParam);
            return RedirectToAction(nameof(Index));
        }
    }
}
