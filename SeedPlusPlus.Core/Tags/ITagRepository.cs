namespace SeedPlusPlus.Core.Tags;

public interface ITagRepository
{
    Task<Result<Tag>> FindById(int id);
    Task<Result<List<Tag>>> GetAllAsync();
    Task<Result<Tag>> AddTag(Tag tag);
    Task<Result<bool>> DeleteTagAsync(int id);
}