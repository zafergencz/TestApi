using TestApi.Application.Models;

namespace TestApi.Application.Interfaces;

public interface ICustomerRepository
{
    Customer? GetById(long customerId);
    Customer? GetByIdentityNoAndStatus(long? identityNo, string? status);
    void Add(Customer transaction);
    void Update(Customer transaction);
    void Delete(Customer transaction);
}