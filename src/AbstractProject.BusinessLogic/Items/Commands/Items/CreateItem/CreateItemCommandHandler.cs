using System;
using System.Threading;
using System.Threading.Tasks;
using AbstractProject.Abstractions;
using AbstractProject.DataAccess;
using AbstractProject.Domain.Tables;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Response<long>>
    {
        private readonly AbstractProjectDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CreateItemCommandHandler(AbstractProjectDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
            _memoryCache = memoryCache;
        }

        public async Task<Response<long>> Handle(
            CreateItemCommand request,
            CancellationToken cancellationToken = default)
        {
            var title = request.Title;
            var description = request.Description;

            var normalizedTitle = title?.Normalize().Trim() ?? string.Empty;
            var normalizedDescription = description?.Normalize().Trim() ?? string.Empty;

            var entity = new ItemEntity(title: normalizedTitle, description: normalizedDescription);
            await CreateItemAsync(entity: entity, cancellationToken: cancellationToken);
            
            return new Response<long>
            {
                Body = entity.Id
            };
        }

        private async Task CreateItemAsync(
            ItemEntity entity,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _dbContext.Items.AddAsync(entity: entity, cancellationToken: cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}