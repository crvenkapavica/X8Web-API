using Microsoft.AspNetCore.Mvc;
using X8WebAPI._dtos.Comment;
using X8WebAPI._models;

namespace X8WebAPI.Repository.IRepository;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    
    Task<Comment?> GetByIdAsync(int id);
    
    Task<Comment> CreateAsync(Comment comment);
    
    Task<Comment?> UpdateAsync(int id, UpsertCommentDto comment);
    
    Task<Comment?> DeleteAsync(int id);
}