using Foundation.Core.Abstractions;
using Foundation.Core.Mediator;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities.Identifiers;

namespace Jobs.Application.Commands;

public record DeleteJob(JobId Id) : Command<DeleteJob.Result>
{
    public record Result : ExecutionResult;

    public class Handler : CommandHandler<DeleteJob, Result>
    {
        private readonly IJobRepository _repository;

        public Handler(IJobRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public override async Task<Result> Handle(DeleteJob request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetById(request.Id);
            await _repository.DeleteAsync(job);
            return new();
        }
    }
}