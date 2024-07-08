using Microsoft.AspNetCore.Mvc;
using X8WebAPI._dtos.Comment;
using X8WebAPI._models;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;
using System;

namespace X8WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comments = await unitOfWork.Comment.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id=int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comment = await unitOfWork.Comment.GetByIdAsync(id);
        return comment != null ? Ok(comment) : NotFound();
    }
    
    [HttpPost("{stockId=int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] UpsertCommentDto commentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!await unitOfWork.Stock.Exists(stockId)) return BadRequest("The stock doesnt exist!");
    
        var commentModel = await unitOfWork.Comment.CreateAsync(commentDto.ToCommentFromDto(stockId));
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }
    
    [HttpPut("{id=int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertCommentDto commentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var commentModel = await unitOfWork.Comment.UpdateAsync(id, commentDto);
        return commentModel != null ? Ok(commentModel.ToCommentDto()) : NotFound();
    }

    [HttpDelete("{id=int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comment = await unitOfWork.Comment.DeleteAsync(id);
        return NoContent();
    }
}