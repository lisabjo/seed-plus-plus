using Microsoft.EntityFrameworkCore;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Exceptions;
using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Data.Repositories;

public class TagRepository : ITagRepository
{
    private readonly SeedPlusPlusContext _context;

    public TagRepository(SeedPlusPlusContext context)
    {
        _context = context;
    }

    public async Task<Result<Tag>> FindById(int id)
    {
        var result = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
        return result is not null ? result : new NotFoundException<Tag>();
    }
    
    public async Task<Result<Tag>> AddTag(Tag tag)
    {
        try
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<Result<bool>> DeleteTagAsync(int id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null)
            return new NotFoundException<Tag>();

        try
        {
            _context.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}