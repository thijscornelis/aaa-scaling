using System.Text.Json.Serialization;

namespace Shared.Core.Abstractions;

public abstract class ExecutionResult
{
    [JsonIgnore]
    public Exception? Exception { get; private set; }

    public string? ExceptionMessage => Exception?.GetBaseException().Message;

    public TimeSpan ElapsedTime { get; private set; }

    internal void SetException(Exception exception) => Exception = exception;

    internal void SetElapsedTime(TimeSpan elapsedTime) => ElapsedTime = elapsedTime;
}