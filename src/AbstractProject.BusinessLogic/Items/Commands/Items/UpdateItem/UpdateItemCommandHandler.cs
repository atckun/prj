using System;
using System.Threading;
using System.Threading.Tasks;
using AbstractProject.Abstractions;
using AbstractProject.DataAccess;
using AbstractProject.Domain.Tables;
using AbstractProject.Implementations.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Response<long>>
    {
        private readonly AbstractProjectDbContext _dbContext;
        
        public UpdateItemCommandHandler(AbstractProjectDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
        }
        
        public async Task<Response<long>> Handle(
            UpdateItemCommand request, 
            CancellationToken cancellationToken = default)
        {
            var id = request.Id;
            var title = request.Title;
            var description = request.Description;

            var normalizedTitle = title?.Normalize().Trim() ?? string.Empty;
            var normalizedDescription = description?.Normalize().Trim() ?? string.Empty;

            var entity = await _dbContext.Items
                .FirstOrDefaultAsync(predicate: x => x.Id == id, cancellationToken: cancellationToken);

            if (entity == null) throw new EntityRecordNotFoundException(entityName: nameof(ItemEntity), entityKey: id);
            
            entity.ChangeTitle(normalizedTitle);
            entity.ChangeDescription(normalizedDescription);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response<long>
            {
                Body = entity.Id
            };
        }
    }
}