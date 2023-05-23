using TestApi.Application.Interfaces;
using TestApi.Persistence.Data;
using TestApi.Application.Models;

namespace TestApi.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Transaction? GetById(long transactionId)
    {
        return _context.Transactions.Find(transactionId);
    }

    public void Add(Transaction item)
    {
        _context.Transactions.Add(item);
        _context.SaveChanges();
    }

    public void Update(Transaction item)
    {
        _context.Transactions.Update(item);
        _context.SaveChanges();
    }

    public void Delete(Transaction item)
    {
        _context.Transactions.Remove(item);
        _context.SaveChanges();
    }

}
