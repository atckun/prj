using System;
using System.Threading.Tasks;
using AbstractProject.Abstractions;
using AbstractProject.Abstractions.Models;
using AbstractProject.BusinessLogic.Items.Commands.Items.CreateItem;
using AbstractProject.BusinessLogic.Items.Commands.Items.DeleteItem;
using AbstractProject.BusinessLogic.Items.Commands.Items.UpdateItem;
using AbstractProject.BusinessLogic.Items.Queries.Items.GetItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbstractProject.Api.Controllers
{
    /// <summary>
    ///     ItemController
    /// </summary>
    [ApiController]
    [Route(template: "api/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(paramName: nameof(mediator));
        }

        [HttpPost]
        [Consumes(contentType: "application/json")]
        public async Task<ActionResult<Response<long>>> Create([FromBody] CreateItemCommand command)
        {
            return Ok(value: await _mediator.Send(request: command));
        }

        [HttpGet]
        public async Task<ActionResult<Response<ItemResponse>>> Get()
        {
            return Ok(value: await _mediator.Send(request: new GetItemsQuery()));
        }

        [HttpPut]
        [Consumes(contentType: "application/json")]
        public async Task<ActionResult> Update([FromBody] UpdateItemCommand command)
        {
            return Ok(value: await _mediator.Send(request: command));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteItemCommand command)
        {
            return Ok(value: await _mediator.Send(request: command));
        }
    }
}