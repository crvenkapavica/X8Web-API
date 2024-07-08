using Microsoft.AspNetCore.Mvc;
using X8WebAPI._dtos.Comment;
using X8WebAPI._models;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;
using System;

namespace X8WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comments = await _unitOfWork.Comment.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id=int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comment = await _unitOfWork.Comment.GetByIdAsync(id);
        return comment != null ? Ok(comment) : NotFound();
    }
    
    [HttpPost("{stockId=int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] UpsertCommentDto commentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await _unitOfWork.Stock.Exists(stockId)) return BadRequest("The stock doesnt exist!");
    
        var commentModel = await _unitOfWork.Comment.CreateAsync(commentDto.ToCommentFromDto(stockId));
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }
    
    [HttpPut("{id=int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertCommentDto commentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var commentModel = await _unitOfWork.Comment.UpdateAsync(id, commentDto);
        return commentModel != null ? Ok(commentModel.ToCommentDto()) : NotFound();
    }

    [HttpDelete("{id=int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comment = await _unitOfWork.Comment.DeleteAsync(id);
        return NoContent();
    }
}