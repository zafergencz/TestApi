using TestApi.Application.Models;

namespace TestApi.Application.Interfaces;

public interface ITransactionRepository
{
    Transaction? GetById(long transactionId);
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
}
