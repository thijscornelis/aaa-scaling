using System.Text.Json.Serialization;

namespace Foundation.Core.Abstractions;

public abstract record ExecutionResult
{
    [JsonIgnore]
    public Exception? Exception { get; private set; }

    public string? ExceptionMessage => Exception?.GetBaseException().Message;

    internal void SetException(Exception exception) => Exception = exception;

    public TimeSpan ElapsedTime { get; private set; }

    internal void SetElapsedTime(TimeSpan elapsedTime) => ElapsedTime = elapsedTime;
}