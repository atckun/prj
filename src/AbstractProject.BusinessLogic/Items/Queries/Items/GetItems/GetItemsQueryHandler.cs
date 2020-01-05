using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbstractProject.Abstractions;
using AbstractProject.Abstractions.Enums;
using AbstractProject.Abstractions.Models;
using AbstractProject.DataAccess;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AbstractProject.BusinessLogic.Items.Queries.Items.GetItems
{
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, Response<IEnumerable<ItemResponse>>>
    {
        private readonly AbstractProjectDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public GetItemsQueryHandler(
            AbstractProjectDbContext dbContext,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(paramName: nameof(memoryCache));
        }

        public async Task<Response<IEnumerable<ItemResponse>>> Handle(
            GetItemsQuery request,
            CancellationToken cancellationToken = default)
        {
            var cache = await _memoryCache.GetOrCreateAsync(
                key: Enum.GetName(enumType: typeof(MemoryCacheKeys), value: MemoryCacheKeys.Items),
                factory: async entry =>
                {
                    var items = await _dbContext.Items
                        .OrderBy(keySelector: x => x.Id)
                        .ToArrayAsync(cancellationToken: cancellationToken);

                    entry.SlidingExpiration = TimeSpan.FromMilliseconds(10);
                    
                    return _mapper.Map<IEnumerable<ItemResponse>>(source: items);
                });

            return new Response<IEnumerable<ItemResponse>>
            {
                Body = cache
            };
        }
    }
}