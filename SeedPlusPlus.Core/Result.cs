namespace SeedPlusPlus.Core;

//TODO: document

/// <summary>
/// Encapsulates a monad that could contain either a T or an Exception.
/// Map ...
/// Match ...
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Result<T>
{
    private readonly T? _value;
    private readonly Exception? _error;
    
    private Result(T value)
    {
        _value = value;
        _error = default;
    }
    private Result(Exception error)
    {
        _value = default;
        _error = error;
    }

    public static Result<T> Create(Func<T> fn)
    {
        try
        {
            return fn();
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public Result<TOut> Map<TOut>(Func<T, TOut> fn)
    {
        if (_error is not null)
            return new Result<TOut>(_error);

        try
        {
            return fn(_value!);
        }
        catch (Exception e)
        {
            return new Result<TOut>(e);
        }
    }
    
    public Result<TOut> Map<TOut>(Func<T, Result<TOut>> fn)
    {
        if (_error is not null)
            return new Result<TOut>(_error);

        try
        {
            return fn(_value!);
        }
        catch (Exception e)
        {
            return new Result<TOut>(e);
        }
    }
    
    public async Task<Result<TOut>> MapAsync<TOut>(Func<T, Task<Result<TOut>>> fn)
    {
        if (_error is not null)
            return new Result<TOut>(_error);

        try
        {
            return await fn(_value!);
        }
        catch (Exception e)
        {
            return new Result<TOut>(e);
        }
    }

    public TOut Match<TOut>(Func<T, TOut> fnSuccess, Func<Exception, TOut> fnFail)
    {
        return _error is null 
            ? fnSuccess(_value!)
            : fnFail(_error);
    }


    public static IEnumerable<TOut> FilterOutErrors<TOut>(IEnumerable<Result<TOut>> items)
    {
        return items
            .Where(item => item._error is null)
            .Select(item => item._value!);
    }
    
    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Exception e) => new(e);
}

public static class ResultExtensions
{
    public static async Task<Result<TOut>> MapAsync<T, TOut>(
        this Task<Result<T>> task,
        Func<T, TOut> fn)
    {
        return (await task).Map(fn);
    }
    
    public static async Task<Result<TOut>> MapAsync<T, TOut>(
        this Task<Result<T>> task,
        Func<T, Task<Result<TOut>>> fn)
    {
        var taskResult = await task;
        return await taskResult.MapAsync(fn);
    }
    
    public static async Task<TOut> MatchAsync<T, TOut>(
        this Task<Result<T>> task, 
        Func<T, TOut> fnSuccess, 
        Func<Exception, TOut> fnFail) => (await task).Match(fnSuccess, fnFail);
}
