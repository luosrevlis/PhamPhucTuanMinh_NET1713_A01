using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO _dao;

        public CustomerRepository(CustomerDAO dao)
        {
            _dao = dao;
        }

        public void AddCustomer(Customer customer) => _dao.Add(customer);

        public void DeleteCustomer(int id) => _dao.Delete(id);

        public Customer? FindCustomerById(int id) => _dao.FindById(id);

        public List<Customer> FindCustomersByName(string name) => _dao.FindByName(name);

        public List<Customer> GetAllCustomers() => _dao.GetAll();

        public void UpdateCustomer(Customer customer) => _dao.Update(customer);
    }
}
