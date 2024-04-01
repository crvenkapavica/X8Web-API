using X8WebAPI._models;

namespace X8WebAPI._data;

public static class SeedData
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Stocks.Any()) return;
        
        db.Stocks.AddRange(new List<Stock>
        {
            new Stock
            {
                Symbol = "TSLA",
                CompanyName = "Tesla",
                Purchase = 125.33m,
                LastDiv = 2.00m,
                Industry = "Automotive",
                MarketCap = 100223
            },
            new Stock
            {
                Symbol = "MSFT",
                CompanyName = "Microsoft",
                Purchase = 261.25m,
                LastDiv = 1.12m,
                Industry = "Technology",
                MarketCap = 240540
            },
            new Stock
            {
                Symbol = "AAPL",
                CompanyName = "Apple",
                Purchase = 195.58m,
                LastDiv = 2.10m,
                Industry = "Technology",
                MarketCap = 100223
            },
            new Stock
            {
                Symbol = "NVDA",
                CompanyName = "NVIDIA",
                Purchase = 205.51m,
                LastDiv = 2.48m,
                Industry = "Hardware",
                MarketCap = 100223
            }
        });
        
        db.SaveChanges();
    }
}