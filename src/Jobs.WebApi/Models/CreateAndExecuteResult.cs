using Jobs.Application.Commands;

namespace Jobs.WebApi.Models;

public record CreateAndExecuteResult(CreateJob.Result? CreateResult, ExecuteJob.Result? ExecuteResult = null);