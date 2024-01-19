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

        public List<Customer> GetAll() => _db.Customers.ToList();

        public Customer? FindById(int id) => _db.Customers.Find(id);

        public List<Customer> FindByName(string name)
            => _db.Customers.Where(customer => (customer.CustomerFullName ?? string.Empty).Contains(name)).ToList();

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
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }
    }
}
