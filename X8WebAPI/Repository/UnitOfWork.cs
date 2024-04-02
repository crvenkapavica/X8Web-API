using X8WebAPI._data;
using X8WebAPI.Repository.IRepository;

namespace X8WebAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IStockRepository Stock { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        Stock = new StockRepository(db);
    }
}