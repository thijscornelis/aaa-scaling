using System.Text.Json.Serialization;

namespace Foundation.Core.Abstractions;

public abstract record ExecutionResult
{
    [JsonIgnore] public Exception? Exception { get; private set; }

    public string? ExceptionMessage => Exception?.GetBaseException().Message;

    public TimeSpan ElapsedTime { get; private set; }

    internal void SetException(Exception exception)
    {
        Exception = exception;
    }

    internal void SetElapsedTime(TimeSpan elapsedTime)
    {
        ElapsedTime = elapsedTime;
    }
}