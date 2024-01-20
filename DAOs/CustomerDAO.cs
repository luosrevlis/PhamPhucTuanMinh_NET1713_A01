using BusinessObjects;

namespace DAOs
{
    public class CustomerDAO
    {
        private readonly FuMiniHotelManagementContext _db;

        public CustomerDAO(FuMiniHotelManagementContext db)
        {
            _db = db;
        }

        public List<Customer> GetAll()
        {
            return _db.Customers
                .Where(customer => customer.CustomerStatus != (byte)Status.Deleted)
                .ToList();
        }

        public Customer? FindById(int id)
        {
            return _db.Customers
                .FirstOrDefault(customer => customer.CustomerId == id && customer.CustomerStatus != (byte)Status.Deleted);
        }

        public List<Customer> FindByName(string name)
        {
            return _db.Customers
                .Where(customer => (customer.CustomerFullName ?? string.Empty).Contains(name) && customer.CustomerStatus != (byte)Status.Deleted)
                .ToList();
        }

        public void Add(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = FindById(id);
            if (customer == null)
            {
                return;
            }
            customer.CustomerStatus = (byte)Status.Deleted;
            Update(customer);
        }
    }
}
