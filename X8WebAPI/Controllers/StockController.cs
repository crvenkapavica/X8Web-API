using Microsoft.AspNetCore.Mvc;
using X8WebAPI._data;
using X8WebAPI._dtos.Stock;
using X8WebAPI.mappers;

namespace X8WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public StockController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _db.Stocks.ToList()
            .Select(s => s.ToStockDto());

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _db.Stocks.Find(id);

        return stock != null ? Ok(stock.ToStockDto()) : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromRequestDto();
        _db.Stocks.Add(stockModel);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
}