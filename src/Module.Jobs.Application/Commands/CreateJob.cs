using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Domain.Entities;
using Module.Jobs.Domain.ValueObjects;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Application.Commands;

public record CreateJob(JobId JobId, UserId UserId) : Command<CreateJob.Result>
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

        public JobId JobId { get; init; }
        public JobStatus? JobStatus { get; init; }
    }

    public class Handler : CommandHandler<CreateJob, Result>
    {
        private readonly IJobRepository _jobRepository;

        public Handler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(CreateJob request, CancellationToken cancellationToken)
        {
            var job = new Job(request.JobId, request.UserId);
            await _jobRepository.AddAsync(job, cancellationToken);
            return new Result(job.Id, job.Status);
        }
    }
}