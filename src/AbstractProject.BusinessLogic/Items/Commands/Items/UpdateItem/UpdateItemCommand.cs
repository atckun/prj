using AbstractProject.Abstractions;
using AbstractProject.Abstractions.Models;
using MediatR;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.UpdateItem
{
    public class UpdateItemCommand : ItemModel, IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
}