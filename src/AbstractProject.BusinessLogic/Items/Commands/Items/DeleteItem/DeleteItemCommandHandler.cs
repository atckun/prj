using System;
using System.Threading;
using System.Threading.Tasks;
using AbstractProject.Abstractions;
using AbstractProject.DataAccess;
using AbstractProject.Domain.Tables;
using AbstractProject.Implementations.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Response<Unit>>
    {
        private readonly AbstractProjectDbContext _dbContext;

        public DeleteItemCommandHandler(AbstractProjectDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
        }

        public async Task<Response<Unit>> Handle(
            DeleteItemCommand request,
            CancellationToken cancellationToken = default)
        {
            var id = request.Id;

            var entity = await _dbContext.Items
                .FirstOrDefaultAsync(predicate: x => x.Id == id, cancellationToken: cancellationToken);

            if (entity == null) throw new EntityRecordNotFoundException(entityName: nameof(ItemEntity), entityKey: id);

            await RemoveItemAsync(entity: entity, cancellationToken: cancellationToken);

            return new Response<Unit>
            {
                Body = Unit.Value
            };
        }

        private async Task RemoveItemAsync(
            ItemEntity entity,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _dbContext.Items.Remove(entity: entity);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}