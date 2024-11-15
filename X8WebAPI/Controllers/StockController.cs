﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X8WebAPI._data;
using X8WebAPI._dtos.Stock;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;

namespace X8WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await unitOfWork.Stock.GetAllAsync();
        return Ok(stocks);
    }

    [HttpGet("{id=int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await unitOfWork.Stock.GetByIdAsync(id);
        
        return stock != null ? Ok(stock) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertStockDto stockDto)
    {
        var stockModel = await unitOfWork.Stock.CreateAsync(stockDto.ToStockFromDto());
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id=int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertStockDto stockDto)
    {
        var stockModel = await unitOfWork.Stock.UpdateAsync(id, stockDto);
        
        return stockModel != null ? Ok(stockModel.ToStockDto()) : NotFound();
    }

    [HttpDelete("{id=int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await unitOfWork.Stock.DeleteAsync(id);

        if (stockModel == null) return NotFound();

        return NoContent();
    }
}