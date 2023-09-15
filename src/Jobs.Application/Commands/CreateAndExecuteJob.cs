using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Commands;

public record CreateAndExecuteJob(JobId JobId, UserId UserId) : Command<CreateAndExecuteJob.Result>
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

    public class Handler : CommandHandler<CreateAndExecuteJob, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public Handler(IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(CreateAndExecuteJob request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId, cancellationToken);
            var job = new Job(request.JobId, user);
            await job.Execute(cancellationToken);
            await _jobRepository.AddAsync(job, cancellationToken);
            return new Result(job.Id, job.Status);
        }
    }
}