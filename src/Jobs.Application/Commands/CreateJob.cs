using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Commands;

public record CreateJob(JobId JobId) : Command<CreateJob.Result>
{
    public record Result : ExecutionResult
    {
        /// <inheritdoc />
        public Result()
        {
        }

        /// <inheritdoc />
        public Result(JobId jobId, JobStatus jobStatus)
        {
            JobId = jobId;
            JobStatus = jobStatus;
        }

        public JobId? JobId { get; init; }
        public JobStatus? JobStatus { get; init; }
    }

    public class Handler : CommandHandler<CreateJob, Result>
    {
        private readonly IJobRepository _repository;

        public Handler(IJobRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(CreateJob request, CancellationToken cancellationToken)
        {
            var job = new Job(request.JobId);
            await _repository.AddAsync(job, cancellationToken);
            return new Result(job.Id, job.Status);
        }
    }
}