using X8WebAPI._dtos.Stock;
using X8WebAPI._models;

namespace X8WebAPI.Repository.IRepository;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpsertStockDto stockDto);
    Task<Stock?> DeleteAsync(int id);
}