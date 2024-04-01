using Microsoft.EntityFrameworkCore;
using X8WebAPI._models;

namespace X8WebAPI._data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Stock> Stocks { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
}