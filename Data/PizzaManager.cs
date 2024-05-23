using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace la_mia_pizzeria_static.Data

{
    public class PizzaManager
    {

        //Metodo nel manager per restituirmi una lista di pizze
        public static List<Pizza> GetAllPizzas()
        {
            using PizzeriaContext db = new PizzeriaContext();
            return db.Pizze.ToList();
        }

        //Metodo nel manager per restituirmi una pizza sola
        public static Pizza GetPizza(long id, bool includeReferences = true)
        {
            using PizzeriaContext db = new PizzeriaContext();
            if (includeReferences)
                return db.Pizze.Where(x => x.Id == id).Include(p => p.Categoria).Include(p => p.Ingredienti).FirstOrDefault();
            
            return db.Pizze.FirstOrDefault(p => p.Id == id);
        }

        public static List<Categoria> GetAllCategorie()
        {
            using PizzeriaContext db = new PizzeriaContext();
            return db.Categorie.ToList();
        }

        public static List<Ingredienti> GetIngredienti()
        {
            using PizzeriaContext db = new PizzeriaContext();
            return db.Ingredienti.ToList();
        }

        //Metodo per aggiungere le pizze al database Pizze
        public static void AggiungiPizza(Pizza pizza, List<String> selectedIngredients)
        {
            using PizzeriaContext db = new PizzeriaContext();
            pizza.Ingredienti = new List<Ingredienti>();

            if (selectedIngredients != null)
            {
                foreach (var ingrediente in selectedIngredients)
                {
                    int id = int.Parse(ingrediente);
                    var ingredienteDb = db.Ingredienti.FirstOrDefault(i => i.Id == id);

                    if (ingredienteDb != null)
                    {
                        pizza.Ingredienti.Add(ingredienteDb);
                    }

                }
            }

            db.Pizze.Add(pizza);
            db.SaveChanges();
        }

        public static bool ModificaPizza(long id, string nome, string descrizione, string fotopath, float prezzo, int? categoriaid, List<string> ingredienti) 
        {
            using PizzeriaContext db = new PizzeriaContext();
            var pizza = db.Pizze.Where(p => p.Id == id).Include(p => p.Ingredienti).FirstOrDefault();

            if (pizza == null)
            {
                return false;
            }
            else 
            { 
                pizza.Nome = nome;
                pizza.Descrizione = descrizione;
                pizza.FotoPath = fotopath;
                pizza.Prezzo = prezzo;
                pizza.CategoriaId = categoriaid;

                pizza.Ingredienti.Clear(); 
                if (ingredienti != null)
                {
                    foreach (var ingrediente in ingredienti)
                    {
                        int ingredienteId = int.Parse(ingrediente);
                        var ingredienteDB = db.Ingredienti.FirstOrDefault(x => x.Id == ingredienteId);

                        if (ingredienteDB != null)
                        { 
                            pizza.Ingredienti.Add(ingredienteDB);
                        }
                    }
                }

                db.SaveChanges();

                return true;
            }
        }

        public static bool EliminaPizza(long id)
        {
            using PizzeriaContext db = new PizzeriaContext();
            var pizzaDaEliminare =  db.Pizze.FirstOrDefault(p => p.Id == id);

            if (pizzaDaEliminare == null)
            {
                return false;
            }
            else 
            {
                db.Pizze.Remove(pizzaDaEliminare);
                db.SaveChanges();
                return true;
            }


        }
    }
}
