using TestApi.Application.Interfaces;
using TestApi.Persistence.Data;
using TestApi.Application.Models;

namespace TestApi.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Customer? GetById(long customerId)
    {
        return _context.Customers.Find(customerId);
    }

    public Customer? GetByIdentityNoAndStatus(long? identityNo, string? status)
    {
        return _context.Customers.FirstOrDefault(element => element.identityNo == identityNo && element.status == status);
    }

    public void Add(Customer item)
    {
        _context.Customers.Add(item);
        _context.SaveChanges();
    }

    public void Update(Customer item)
    {
        _context.Customers.Update(item);
        _context.SaveChanges();
    }

    public void Delete(Customer item)
    {
        _context.Customers.Remove(item);
        _context.SaveChanges();
    }

}
