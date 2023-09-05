using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Foundation.Core.Mediator;
using Jobs.Application.Contracts;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;

namespace Jobs.Application.Queries;

public record GetJob(JobId Id) : Query<GetJob.Result>
{
    public record Result : ExecutionResult
    {
        /// <inheritdoc />
        public Result()
        {
        }

        /// <inheritdoc />
        public Result(JobDto job)
        {
            Job = job;
        }

        public JobDto? Job { get; init; }
    }

    public class Handler : QueryHandler<GetJob, Result>
    {
        private readonly IReadOnlyRepository<Job, JobId> _repository;

        public Handler(IReadOnlyRepository<Job, JobId> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(GetJob request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetById(request.Id, cancellationToken);
            return new Result((JobDto)job);
        }
    }
}