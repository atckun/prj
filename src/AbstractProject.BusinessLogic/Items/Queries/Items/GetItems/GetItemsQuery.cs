using System.Collections.Generic;
using AbstractProject.Abstractions;
using AbstractProject.Abstractions.Models;
using MediatR;

namespace AbstractProject.BusinessLogic.Items.Queries.Items.GetItems
{
    public class GetItemsQuery : IRequest<Response<IEnumerable<ItemResponse>>>
    {
    }
}