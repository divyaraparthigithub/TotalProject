using Task20_consumewebapioftask11_.Models;

namespace Task20_consumewebapioftask11_.ServiceLayer
{
    public interface ICustomerService
    {
        //IEnumerable<Customer> GetCustomerList();
        public IEnumerable<Product> ProductList();
        public IEnumerable<Customer> CustomerLists();
        public Task<bool> DeleteCustomerAsync(int id);

    }
}
