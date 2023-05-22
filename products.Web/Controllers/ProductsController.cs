using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using products.Application.products.Queries;
using products.Application.Products.Commands;

namespace products.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Manager , Administrator")]
[ApiController]
public class ProductsController : ApiControllersBase
{
    private IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public ProductsController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _env = environment; 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductsQuery());
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(string id)
    {
        var response = await _mediator.Send(new GetProductByIdQuery { Id = id});
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]CreateProductCommand command)
    {
        command.UserId = CurrentUserId;
        command.WebRootPath = _env.WebRootPath;
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromForm] UpdateproductCommand command)
    {
        command.UserId = CurrentUserId;
        command.WebRootPath = _env.WebRootPath;
        var response = await _mediator.Send(command);
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _mediator.Send(new DeleteProductCommand { Id =id,UserId=CurrentUserId});
        if (response.Success) return Ok(response);
        else return BadRequest(response);
    }


}
