using Application.Interfaces;
using Application.Services;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasket _basketRepository;

        public BasketController(IBasket basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<IActionResult> Index()
        {
            var baskets = await _basketRepository.GetAllAsync();
            return View(baskets);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Basket basket)
        {
            if (basket == null) return BadRequest();

            basket.Amount = 1;
            await _basketRepository.AddAsync(basket);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _basketRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateAmountAjax(int id, int amount)
        {
            _basketRepository.UpdateAmount(id, amount);
            var newTotal = _basketRepository.GetTotal(id);
            return Json(newTotal);
        }

    }
}
