using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X8WebAPI._data;
using X8WebAPI._dtos.Stock;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;

namespace X8WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StockController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _unitOfWork.Stock.GetAllAsync();
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _unitOfWork.Stock.GetByIdAsync(id);
        
        return stock != null ? Ok(stock.ToStockDto()) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertStockRequestDto stockDto)
    {
        var stockModel = await _unitOfWork.Stock.CreateAsync(stockDto.ToStockFromRequestDto());
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertStockRequestDto stockDto)
    {
        var stockModel = await _unitOfWork.Stock.UpdateAsync(id, stockDto);

        if (stockModel == null) return NotFound();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _unitOfWork.Stock.DeleteAsync(id);

        if (stockModel == null) return NotFound();

        return NoContent();
    }
}