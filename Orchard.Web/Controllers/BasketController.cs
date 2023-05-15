using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Services.Domain;

namespace Orchard.Web.Controllers
{
    [Route("api/basket")]
    [Produces("application/json")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IOrchardService _orchardService;

        public BasketController(IOrchardService orchardService)
        {
            _orchardService = orchardService;
        }

        [HttpGet("{includeFruit:bool}/{id:int}")]
        public async Task<BasketModel?> GetBasketById(bool includeFruit, int id)
        {
            return await _orchardService.GetBasketById(id, includeFruit);
        }

        [HttpGet("{includeFruit:bool}")]
        public async Task<List<BasketModel>> GetAllBaskets(bool includeFruit)
        {
            return await _orchardService.GetAllBaskets(includeFruit);
        }

        [HttpPost("create")]
        public async Task<BasketModel> CreateBasket(BasketModel basket)
        {
            return await _orchardService.CreateBasket(basket);
        }
        
        [HttpPut("{id:int}/update")]
        // id is unused here - this is REST standard to include resource id in url route.
        public async Task<bool> UpdateBasket(int id, BasketModel basket)
        {
            return await _orchardService.UpdateBasket(basket);
        }
        
        [HttpDelete("{id:int}/delete")]
        // id is unused here - this is REST standard to include resource id in url route.
        public async Task<bool> DeleteBasket(int id)
        {
            return await _orchardService.DeleteBasket(id);
        }
    }
}
