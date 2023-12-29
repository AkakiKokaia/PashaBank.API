using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PashaBank.Domain.Interfaces;

namespace PersonIdentificationDirectory.Utility.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly ITransactionBehaviour _transaction;

        public TransactionBehaviour(ITransactionBehaviour transaction,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _transaction = transaction ?? throw new ArgumentException(nameof(IUnitOfWork));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                var strategy = _transaction.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _transaction.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _transaction.CommitTransactionAsync(cancellationToken);

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");

                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}; \n {ex.ToString()}");

                await _transaction.RollbackTransaction();
                throw;
            }
        }
    }
}
