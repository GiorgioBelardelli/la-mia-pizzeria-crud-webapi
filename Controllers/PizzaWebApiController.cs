using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaWebApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPizzas(int id)
        {
            var pizze = PizzaManager.GetPizza(id);
            return Ok(pizze);
        }

        public IActionResult GetPizzaById()
        {
            var pizza = PizzaManager.GetAllPizzas();
            if (pizza == null)
            {
                return NotFound();  
            }
            
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult CreaPizza([FromBody] Pizza pizza)
        {
            PizzaManager.AggiungiPizza(pizza, null);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePizza(int id, [FromBody] Pizza pizza)
        {
            var pizzaDaModificare = PizzaManager.GetPizza(id);
            if (pizzaDaModificare == null) 
            {
                return NotFound(); 
            }
            
            PizzaManager.ModificaPizza(id, pizza.Nome, pizza.Descrizione, pizza.FotoPath, pizza.Prezzo, pizza.CategoriaId, null);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult UpdatePizza(int id)
        {

            if (PizzaManager.EliminaPizza(id))
            {
                return Ok();
            }
                return NotFound();
        }

    }

}
