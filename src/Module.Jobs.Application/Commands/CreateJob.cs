using Shared.Core.Abstractions;

namespace Module.Jobs.Application.Commands;

public class CreateJob : Command<CreateJob.Result>
{

    public class Result : ExecutionResult{}
}