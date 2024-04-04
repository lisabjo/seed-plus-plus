namespace SeedPlusPlus.Core;

#if false
public readonly struct Optional<T>
{
    private readonly T? _value;
    private readonly bool _hasValue;
    
    public Optional()
    {
        _value = default;
        _hasValue = false;
    }
    private Optional(T value)
    {
        _value = value;
        _hasValue = true;
    }

    public Optional<TOut> Map<TOut>(Func<T, TOut> fn)
    {
        return _hasValue ? fn(_value!) : Optional<TOut>.Empty();
    }
    
    public async Task<Optional<TOut>> Map<TOut>(Func<T, Task<Optional<TOut>>> fn)
    {
        return _hasValue ? await fn(_value!) : Optional<TOut>.Empty();
    }
    
    public TOut Match<TOut>(Func<T, TOut> fnSuccess, Func<TOut> fnEmpty)
    {
        return _hasValue ? fnSuccess(_value!) : fnEmpty();
    }
    
    public void Do(Action<T> fnSuccess, Action fnEmpty)
    {
        if (_hasValue)
            fnSuccess(_value!);
        else 
            fnEmpty();
    }
    
    public static Optional<T> Empty() => new();
    
    public static implicit operator Optional<T>(T value) => new(value);
}

public static class Extensions
{
    public static async Task<Optional<TOut>> MapAsync<T, TOut>(
        this Task<Optional<T>> task,
        Func<T, TOut> fn)
    {
        return (await task)
            .Map(fn)
            .Match(
                x => x,
                Optional<TOut>.Empty
            );
    }
    
    public static async Task<TOut> MatchAsync<T, TOut>(
        this Task<Optional<T>> task,
        Func<T, TOut> fnSuccess,
        Func<TOut> fnEmpty)
    {
        return (await task).Match(fnSuccess, fnEmpty);
    }

}
#endif