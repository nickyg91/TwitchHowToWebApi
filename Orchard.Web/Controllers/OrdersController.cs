using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Models.Authentication;
using Orchard.Web.ApplicationServices;

namespace Orchard.Web.Controllers;
[Route("api/orders")]
[Authorize]
[Produces("application/json")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersApplicationService _orderService;
    private readonly IAuthenticatedUser _user;

    public OrdersController(IOrdersApplicationService orderService, IAuthenticatedUser user)
    {
        _orderService = orderService;
        _user = user;
    }
    
    [HttpPost("submit")]
    public async Task<BasketModel> SubmitOrder(BasketModel basket)
    {
        return await _orderService.SubmitOrder(basket, _user.Id);
    }

    [HttpGet("")]
    public async Task<List<BasketModel>> GetOrdersForUser()
    {
        return await _orderService.GetOrdersForUser(_user.Id);
    }

    [HttpGet("{orderId}/details")]
    public async Task<List<BasketFruitModel>> GetDetails(int orderId)
    {
        return await _orderService.GetMoreOrderDetails(orderId, _user.Id);
    }

    [HttpPut("{orderId}/cancel")]
    public async Task<bool> CancelOrder(int orderId)
    {
        return await _orderService.CancelOrder(orderId, _user.Id);
    }
}
