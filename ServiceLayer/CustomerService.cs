using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task20_consumewebapioftask11_.Models;

namespace Task20_consumewebapioftask11_.ServiceLayer
{
    public class CustomerService:ICustomerService
    {
        private readonly ProductDbContext _dbcontext;
        public CustomerService(ProductDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        //public async Task<IActionResult> CustomerLists()
        //{
        //    try
        //    {
        //        var res = (from c in _productdb.Customer
        //                   join p in _productdb.Product on c.ProductId equals p.Id
        //                   join g in _productdb.Gender on c.GenderId equals g.Id
        //                   select new
        //                   {
        //                       c.Id,
        //                       c.Name,
        //                       c.Address,
        //                       c.Email,
        //                       c.Phone,
        //                       ProductName = p.ProductName,
        //                       GenderName = g.GenderName
        //                   }).AsEnumerable();
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"An error occurred: {ex.Message}");
        //    }
        //    //return Ok();
        //}


        public IEnumerable<Product> ProductList()
        {
            return _dbcontext.Product.ToList();
        }

        public IEnumerable<Customer> CustomerLists()
        {
           
                var res = (from c in _dbcontext.Customer
                           join p in _dbcontext.Product on c.ProductId equals p.Id
                           join g in _dbcontext.Gender on c.GenderId equals g.Id
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
            return (IEnumerable<Customer>)res;

        }

        //static List<int> list = new List<int>();

        //public IEnumerable<Customer> GetCustomerList()
        //{
        //    var l=_dbcontext.Customer.ToList();
        //    GetProductList();
        //    for (int i = 0; i < l.Count; i++)
        //        {
        //        list.Add(l[i].ProductId);
        //        }




        //    return l;
        //}
        //public IEnumerable<Product> GetProductList() 
        //{
        //    IEnumerable<Product> pr=_dbcontext.Product.ToList();
        //    //for (int i = 0; i < list.Count; i++)
        //    //{
        //    //    //for (int j = 0; j < pr.Count; j++)
        //    //    //{
        //    //    //    if (list[i] == pr[j].Id)
        //    //    //    {
        //    //    //        //l[i].ProductName = pr[j].ProductName;
        //    //    //    }
        //    //    //}

        //    //}
        //    return pr;
        //}
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var existingCustomer = await _dbcontext.Customer.FindAsync(id);
                if (existingCustomer == null)
                {
                    return false;
                }

                _dbcontext.Customer.Remove(existingCustomer);
                await _dbcontext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }




}
