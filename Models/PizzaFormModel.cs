using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Categoria>? Categorie { get; set; }

        public List<SelectListItem>? Ingredienti { get; set; } //elementi dei tag selezionabili della select
        public List<string>? IngredientiSelezionati { get; set; } //gli ID degli elementi effettivamente selezionati 
        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Categoria>? categorie)
        {
            this.Pizza = pizza;
            this.Categorie = categorie;
            IngredientiSelezionati = new List<string>();
            if (Pizza.Ingredienti != null)
            {
                foreach (var i in Pizza.Ingredienti) 
                {
                    IngredientiSelezionati.Add(i.Id.ToString());
                }
            }
        }

        public void CreaIngredienti()
        {
            this.Ingredienti = new List<SelectListItem>();

            if (this.IngredientiSelezionati == null)
            {
                this.IngredientiSelezionati = new List<string>();
            }

            var ingredientiDB = PizzaManager.GetIngredienti();

            foreach (var ingrediente in ingredientiDB)
            {
                bool isSelected = this.IngredientiSelezionati.Contains(ingrediente.Id.ToString());

                this.Ingredienti.Add(new SelectListItem()
                {
                    Text = ingrediente.Nome,
                    Value = ingrediente.Id.ToString(),
                    Selected = isSelected
                });
                //if (isSelected)
                //    this.IngredientiSelezionati.Add(ingrediente.Id.ToString());

            }
        }
    }
}
