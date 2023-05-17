using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Models.Authentication;
using Orchard.Services.Domain;

namespace Orchard.Web.Controllers
{
    [Route("api/basket")]
    [Produces("application/json")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IOrchardService _orchardService;
        private readonly IAuthenticatedUser _authenticatedUser;

        public BasketController(IOrchardService orchardService, IAuthenticatedUser authenticatedUser)
        {
            _orchardService = orchardService;
            _authenticatedUser = authenticatedUser;
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
            return await _orchardService.SubmitOrder(basket);
        }
        
        [HttpPut("{id:int}/cancel")]
        public async Task<bool> CancelOrder(int id)
        {
            return await _orchardService.CancelOrder(id, _authenticatedUser.Id);
        }
    }
}
