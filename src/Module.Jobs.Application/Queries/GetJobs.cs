using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Module.Jobs.Application.Contracts;
using Module.Jobs.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Application.Queries;

public record GetJobs : Query<GetJobs.Result>
{
    public record Result : ExecutionResult
    {
        /// <inheritdoc />
        public Result()
        {
            Jobs = Array.Empty<JobDto>();
        }

        /// <inheritdoc />
        public Result(JobDto[] jobs)
        {
            Jobs = jobs;
        }

        public JobDto[] Jobs { get; init; }
    }

    public class Handler : QueryHandler<GetJobs, Result>
    {
        private readonly IReadOnlyRepository<Job, JobId> _repository;

        public Handler(IReadOnlyRepository<Job, JobId> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public override Task<Result> Handle(GetJobs request, CancellationToken cancellationToken)
        {
            var jobs = _repository.GetQueryable().ToList()
                .Select(job => new JobDto(job.Id, job.Status, job.CreatedOn, job.ModifiedOn)).ToArray();
            return Task.FromResult(new Result(jobs));
        }
    }
}