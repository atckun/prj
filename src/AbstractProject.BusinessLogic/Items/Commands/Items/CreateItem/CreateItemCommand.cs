using AbstractProject.Abstractions;
using AbstractProject.Abstractions.Models;
using MediatR;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.CreateItem
{
    public class CreateItemCommand : ItemModel, IRequest<Response<long>>
    {
    }
}