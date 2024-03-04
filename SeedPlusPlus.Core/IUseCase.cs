namespace SeedPlusPlus.Core;

public interface IUseCase<in TInput, TResult>
{
    Task<TResult> Handle(TInput input);
}