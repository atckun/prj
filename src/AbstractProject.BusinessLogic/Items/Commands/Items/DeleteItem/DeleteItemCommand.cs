using AbstractProject.Abstractions;
using MediatR;

namespace AbstractProject.BusinessLogic.Items.Commands.Items.DeleteItem
{
    public class DeleteItemCommand : IRequest<Response<Unit>>
    {
        public long Id { get; set; }
    }
}