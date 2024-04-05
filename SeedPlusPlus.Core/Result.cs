namespace SeedPlusPlus.Core;

/// <summary>
/// Result{T} abstracts from the errors that might occur under manipulation of T,
/// wrapping T in a type that allows you to operate on T as through there are no errors.
/// </summary>
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
    
    public static async Task<Result<T>> CreateAsync(Func<Task<T>> fn)
    {
        try
        {
            return await fn();
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    /// <summary>
    /// Applies the specified function to the wrapped value,
    /// unless it is an error in which case it short-circuits
    /// and propagates the error. If the function throws and error
    /// then an error Result is created.
    /// </summary>
    public Result<TOut> Map<TOut>(Func<T, TOut> fn)
    {
        if (_error is not null)
            return _error;

        try
        {
            return fn(_value!);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    /// <summary>
    /// Also known as flat map. The function fn returns a Result,
    /// therefore it is not wrapped in another Result.
    /// </summary>
    public Result<TOut> Map<TOut>(Func<T, Result<TOut>> fn)
    {
        if (_error is not null)
            return _error;

        try
        {
            return fn(_value!);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    /// <summary>
    /// The function fn returns a Task, therefore it is not wrapped in a Result.
    /// </summary>
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

    /// <summary>
    /// Applies the success function to the value, or the fail function to the error.
    /// </summary>
    public TOut Match<TOut>(Func<T, TOut> fnSuccess, Func<Exception, TOut> fnFail)
    {
        return _error is null 
            ? fnSuccess(_value!)
            : fnFail(_error);
    }

    /// <summary>
    /// Returns a new collection of the filtered values from a collection that
    /// might contain Results with values and with errors.
    /// </summary>
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
