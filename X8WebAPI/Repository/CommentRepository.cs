using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X8WebAPI._data;
using X8WebAPI._dtos.Comment;
using X8WebAPI._models;
using X8WebAPI.Repository.IRepository;

namespace X8WebAPI.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _db;
    
    public CommentRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _db.Comments.Include(c => c.Stock).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _db.Comments.Include(c => c.Stock).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
        
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, UpsertCommentDto commentDto)
    {
        var commentModel = await _db.Comments.FindAsync(id);

        if (commentModel == null) return null;
        
        commentModel.Title = commentDto.Title;
        commentModel.Content = commentDto.Content;
        commentModel.CreatedOn = commentDto.CreatedOn;
        commentModel.StockId = commentDto.StockId;

        await _db.SaveChangesAsync();

        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _db.Comments.FindAsync(id);

        if (commentModel == null) return null;

        _db.Comments.Remove(commentModel);
        await _db.SaveChangesAsync();

        return commentModel;
    }
}