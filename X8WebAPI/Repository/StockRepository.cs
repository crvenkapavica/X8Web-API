using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X8WebAPI._data;
using X8WebAPI._dtos.Stock;
using X8WebAPI._models;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;

namespace X8WebAPI.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _db;
    
    public StockRepository(ApplicationDbContext db)
    {
        _db = db;
    }
        
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _db.Stocks.Include(c => c.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _db.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _db.Stocks.AddAsync(stock);
        await _db.SaveChangesAsync();
        
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpsertStockDto stockDto)
    {
        var stockModel = await _db.Stocks.FindAsync(id);

        if (stockModel == null) return null;
        
        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;

        await _db.SaveChangesAsync();

        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _db.Stocks.FindAsync(id);

        if (stockModel == null) return null;

        _db.Stocks.Remove(stockModel);
        await _db.SaveChangesAsync();

        return stockModel;
    }

    public Task<bool> Exists(int id)
    {
        return _db.Stocks.AnyAsync(s => s.Id == id);
    }
}