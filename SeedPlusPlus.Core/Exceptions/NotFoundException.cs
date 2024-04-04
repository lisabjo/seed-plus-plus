namespace SeedPlusPlus.Core.Exceptions;

public class NotFoundException<TEntity> : Exception
{
    public Type Type => typeof(TEntity);

    public NotFoundException(string? message = null, Exception? innerException = null) : base(message, innerException)
    {
    }
}
