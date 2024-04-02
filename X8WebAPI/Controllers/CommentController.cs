using Microsoft.AspNetCore.Mvc;
using X8WebAPI._dtos.Comment;
using X8WebAPI._models;
using X8WebAPI.Mappers;
using X8WebAPI.Repository.IRepository;

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
        var comments = await _unitOfWork.Comment.GetAllAsync();

        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _unitOfWork.Comment.GetByIdAsync(id);
        
        return comment != null ? Ok(comment) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertCommentDto commentDto)
    {
        var commentModel = await _unitOfWork.Comment.CreateAsync(commentDto.ToCommentFromDto());

        return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertCommentDto commentDto)
    {
        var commentModel = await _unitOfWork.Comment.UpdateAsync(id, commentDto);
        
        if (commentModel == null) return NotFound();
        
        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _unitOfWork.Comment.DeleteAsync(id);

        return NoContent();
    }
}