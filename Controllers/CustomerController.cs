using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Task20_consumewebapioftask11_.Models;
using Task20_consumewebapioftask11_.ServiceLayer;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Task20_consumewebapioftask11_.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using log4net;


namespace Task20_consumewebapioftask11_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //private  ICustomerService _customerservice;
       // private ICustomerService @object;
        private readonly ProductDbContext _productdb;
        //private readonly IConfiguration _configuration;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CustomerController));
       

        public CustomerController( ProductDbContext productdb)
        {
            //_customerservice = customerservice;
            _productdb = productdb;
            //_configuration = configuration;
            //this.@object = @object;
        }

        //public CustomerController(ICustomerService @object)
        //{
        //    this.@object = @object;
        //}

        //public CustomerController(ICustomerService @object)
        //{
        //    this.@object = @object;
        //}

        [HttpPost("Creates")]
        public IActionResult Creates(Customer customerDto)
        {

            return Ok(customerDto);
        }

        [HttpGet("ProductList")]
        public IEnumerable<Product> ProductList()
        {

            //var Products = _customerservice.ProductList();
            return _productdb.Product.ToList();
        }
        //[HttpGet("CustomerList")]

        //public IActionResult CustomerList()
        //{
        //    var res = (from c in _productdb.Customer
        //               select new
        //               {
        //                   c.Id,
        //                   c.Name,
        //                   c.Address,
        //                   c.Email,
        //                   c.Phone,

        //    //ProductName = _productdb.Product
        //    //               .Where(p => p.Id == c.ProductId)
        //    //               .Select(p => p.ProductName).ToList(),
        //    //               GenderName = _productdb.Gender
        //    //               .Where(g => g.Id == c.GenderId)
        //    //               .Select(p => p.GenderName).ToList()



        //               }).AsEnumerable();
        //    return Ok(res);

        //}
        //[HttpGet("CustomerLists")]
        //public IEnumerable<Customer> CustomerLists()
        //{
        //    var customerList = _customerservice.CustomerLists();
        //    return customerList;
        //}
        [HttpGet("CustomerLists")]
        public async Task<IActionResult> CustomerLists()
        {
            //try
            //{
                var res = (from c in _productdb.Customer
                           join p in _productdb.Product on c.ProductId equals p.Id
                           join g in _productdb.Gender on c.GenderId equals g.Id
                           select new
                           {
                               c.Id,
                               c.Name,
                               c.Address,
                               c.Email,
                               c.Phone,
                               ProductName = p.ProductName,
                               GenderName = g.GenderName
                           }).AsEnumerable();
                return Ok(res);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest($"An error occurred: {ex.Message}");
            //}
            //return Ok(res);
        }

        [HttpGet("GetDetailsById")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var product = _productdb.Customer.Find(id);
            try
            {
                //var product = _productdb.Product.Find(id);
                if (product == null)
                {
                    throw new Exception();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred in Create method: {ex.Message}", ex);
                return BadRequest($"An error occurred: {ex.Message}");
            }
            return Ok(product);
        }
        //[HttpPost("CreateCustomer")]
        ////[HttpPost]
        //public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        //{
        //    _productdb.Customer.Add(customer);
        //    await _productdb.SaveChangesAsync();

        //    return Ok(customer);
        //}

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
        //    if (string.IsNullOrEmpty(customerDto.ProductName) || string.IsNullOrEmpty(customerDto.GenderName))
        //    {
        //        return BadRequest("ProductName and GenderName cannot be null or empty");
        //    }

        //    Product product = await _productdb.Product.FirstOrDefaultAsync(p => p.ProductName == customerDto.ProductName);
        //    Gender gender = await _productdb.Gender.FirstOrDefaultAsync(g => g.GenderName == customerDto.GenderName);

            //if (product == null || gender == null)
            //{
            //    return BadRequest("Invalid ProductName or GenderName");
            //}
            var customer = new Customer
            {
                Name = customerDto.Name,
                Address = customerDto.Address,
                Phone = customerDto.Phone,
                Email = customerDto.Email,
                ProductId = customerDto.ProductId,
                GenderId = customerDto.GenderId
            };
            _productdb.Customer.Add(customer);
            await _productdb.SaveChangesAsync();
            return Ok(customer);
        }


        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] CustomerDto customerDto)
        {
            try
            {
                var existingCustomer = await _productdb.Customer.FindAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }
                //Product product = await _productdb.Product.FirstOrDefaultAsync(p => p.ProductName == customerDto.ProductName);
                //Gender gender = await _productdb.Gender.FirstOrDefaultAsync(g => g.GenderName == customerDto.GenderName);
                //if (product == null || gender == null)
                //{

                //    return BadRequest("Invalid ProductName or GenderName");
                //}
                existingCustomer.Name = customerDto.Name;
                existingCustomer.Address = customerDto.Address;
                existingCustomer.Phone = customerDto.Phone;
                existingCustomer.Email = customerDto.Email;
                existingCustomer.ProductId = customerDto.ProductId;
                existingCustomer.GenderId = customerDto.ProductId;
                _productdb.Entry(existingCustomer).State = EntityState.Modified;
                await _productdb.SaveChangesAsync();
                return Ok(existingCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingCustomer = await _productdb.Customer.FindAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }
                _productdb.Customer.Remove(existingCustomer);
                await _productdb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }

            return NoContent();
        }
        //[HttpDelete("Deleted")]
        //public async Task<IActionResult> Deleted(int id)
        //{
        //    var isDeleted = await _customerservice.DeleteCustomerAsync(id);
        //    if (!isDeleted)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}


        //[HttpGet("GetProductList")]
        //public List<SelectListItem> GetProductList()
        //{
        //    var productList = _productdb.Product
        //        .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
        //    return productList;
        //}
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var result = (from p in _productdb.Product
                              select new
                              {
                                  p.Id,
                                  p.ProductName,

                              }
                             ).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("GetProductList")]
        public ActionResult<IList<SelectListItem>> GetProductList()
        {
            List<SelectListItem> ProductList = new List<SelectListItem>();

            try
            {
                var products = _productdb.Product.ToList();

                foreach (var product in products)
                {
                    ProductList.Add(new SelectListItem
                    {
                       Value = product.Id.ToString(),
                       Text = product.ProductName
                    });
                }

                return ProductList;
            }
            catch (Exception ex)
            {
                
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
    //[HttpGet("dropdown")]
    //    public async Task <IActionResult> GetProductProductList()
    //    {
    //        var productList = GetAllProducts()
    //            .Select(product => new SelectListItem
    //            {
    //                Value = product.Id.ToString(),
    //                Text = product.ProductName
    //            })
    //            .ToList();

    //        return Ok(productList);
    //    }
    //}
}

///////////////////////////////////////////////////////////////////////////////////////////////////logging methods////////////////////////////////////////////////////







