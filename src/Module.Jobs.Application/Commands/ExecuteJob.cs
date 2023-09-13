using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Domain.ValueObjects;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Application.Commands;

public record ExecuteJob(JobId JobId) : Command<ExecuteJob.Result>
{
    public record Result : ExecutionResult
    {
        /// <inheritdoc />
        public Result()
        {
            JobId = JobId.Empty;
        }

        public Result(JobId jobId, JobStatus jobStatus, TimeSpan jobDuration)
        {
            JobId = jobId;
            JobStatus = jobStatus;
            JobDuration = jobDuration;
        }

        public JobId JobId { get; init; }
        public TimeSpan JobDuration { get; init; }
        public JobStatus? JobStatus { get; init; }
    }

    public class Handler : CommandHandler<ExecuteJob, Result>
    {
        private readonly IJobRepository _jobRepository;

        public Handler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(ExecuteJob command, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetById(command.JobId, cancellationToken);
            var seconds = await job.Execute(cancellationToken);
            var find = _jobRepository.GetQueryable(x => x.Id.Equals(job.Id));
            return new Result(job.Id, job.Status, TimeSpan.FromSeconds(seconds));
        }
    }
}