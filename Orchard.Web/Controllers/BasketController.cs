using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchard.Grpc;
using Orchard.Models;
using Orchard.Models.Authentication;
using Orchard.Services.Domain;
using OrderStatus = Orchard.Core.Enums.OrderStatus;

namespace Orchard.Web.Controllers
{
    [Route("api/basket")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IOrchardService _orchardService;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly Orders.OrdersClient _ordersClient;

        public BasketController(IOrchardService orchardService, IAuthenticatedUser authenticatedUser, Orders.OrdersClient ordersClient)
        {
            _orchardService = orchardService;
            _authenticatedUser = authenticatedUser;
            _ordersClient = ordersClient;
        }

        [HttpGet("{id:int}")]
        public async Task<BasketModel?> GetBasketById(int id)
        {
            return await _orchardService.GetBasketById(id, _authenticatedUser.Id);
        }

        [HttpGet("")]
        public async Task<List<BasketModel>> GetAllBaskets()
        {
            return await _orchardService.GetAllBasketsByUserId(_authenticatedUser.Id);
        }

        [HttpPost("submit")]
        public async Task<BasketModel> SubmitOrder(BasketModel basket)
        {
            var request = new OrderRequest
            {
                Fruit =
                {
                    basket.Fruit.Select(x => new OrderRequest.Types.BasketFruit
                    {
                        Amount = x.Amount,
                        FruitId = x.FruitId
                    })
                },
                UserId = _authenticatedUser.Id
            };
            
            var orderResult = await _ordersClient.SubmitOrderAsync(request);

            return new BasketModel
            {
                Id = orderResult.Id,
                OrderStatus = (OrderStatus)orderResult.OrderStatus
            };
        }
        
        [HttpPut("{id:int}/cancel")]
        public async Task<bool> CancelOrder(int id)
        {
            return await _orchardService.CancelOrder(id, _authenticatedUser.Id);
        }
    }
}
