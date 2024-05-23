using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Index()
        {
                //List<Pizza> pizze = AggiungiPizze();

                //foreach (var pizza in pizze)
                //{
                //db.Pizze.Add(pizza);
                //}

                //db.SaveChanges();

                List<Pizza> pizze = PizzaManager.GetAllPizzas();
                return View("Index", pizze);
        }

        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Show(int id)
        {

            try
            {
                var pizza = PizzaManager.GetPizza(id);
                if (pizza != null)
                    return View(pizza);
                else
                    return NotFound();
                    //return View("Errore", new ErroreViewModel($"La pizza {id} non è stata trovata!"));
            }
            catch (Exception e)
            {
                //return View("Errore", new ErroreViewModel(e.Message));
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            PizzaFormModel model = new PizzaFormModel();   
            model.Pizza = new Pizza();
            model.Categorie = PizzaManager.GetAllCategorie();
            model.CreaIngredienti();
           
            return View(model);        
        }

        //Action che fornisce la view con la form per creare una pizza
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid) 
            {
                List<Categoria> categorie = PizzaManager.GetAllCategorie();
                data.Categorie = categorie;
                data.CreaIngredienti();
                return View("Create", data);
            }
            PizzaManager.AggiungiPizza(data.Pizza, data.IngredientiSelezionati);
            return RedirectToAction("Index");

        }

        

        //Action che fornisce la view con la form per modificare una pizza
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {
            var pizzaDaModificare = PizzaManager.GetPizza(id);

            if (pizzaDaModificare == null)
            {
                return NotFound();
            }
            else 
            {
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = pizzaDaModificare;
                model.Categorie = PizzaManager.GetAllCategorie();
                model.CreaIngredienti();


                return View(model);
            
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categorie = PizzaManager.GetAllCategorie();
                data.CreaIngredienti();
                return View("Update", data);
            }


            if(PizzaManager.ModificaPizza(id, data.Pizza.Nome, data.Pizza.Descrizione, data.Pizza.FotoPath, data.Pizza.Prezzo, data.Pizza.CategoriaId, data.IngredientiSelezionati))
            {
                return RedirectToAction("Index");
            }
            else 
            {
                return NotFound();
            }

        }

        //Action per eliminare una pizza
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            if (PizzaManager.EliminaPizza(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

        }

        //private List<Pizza> AggiungiPizze()
        //{
        //List<Pizza> listaPizze = new List<Pizza>
        //{
        //new Pizza { Nome = "Margherita", Descrizione = "Pomodoro, Mozzarella, Basilico", FotoPath = "https://images.prismic.io/eataly-us/ed3fcec7-7994-426d-a5e4-a24be5a95afd_pizza-recipe-main.jpg?auto=compress,format", Prezzo = 5.99f },
        //new Pizza { Nome = "Boscaiola", Descrizione = "Mozzarella, Salsiccia, Funghi", FotoPath = "https://www.alfaforni.com/wp-content/uploads/2018/08/pizza-boscaiola-wild-traditional-mushroom-pizza.jpg", Prezzo = 8.99f },
        //new Pizza { Nome = "Parmigiana", Descrizione = "Pomodoro, Mozzarella, Melanzane, Parmigiano, Basilico", FotoPath = "https://wips.plug.it/cips/buonissimo.org/cms/2023/01/pizza-con-parmigiana-di-melanzane.jpg", Prezzo = 7.99f },
        //new Pizza { Nome = "Capricciosa", Descrizione = "Pomodoro, Mozzarella, Funghi, Prosciutto Crudo, Uovo, Olive", FotoPath = "https://www.italianmenumaster.com/images/67.jpg", Prezzo = 8.99f },

        //};

        //return listaPizze;
        //}
    }
}
