namespace X8WebAPI.Repository.IRepository;

public interface IUnitOfWork
{
    public IStockRepository Stock { get; }
}