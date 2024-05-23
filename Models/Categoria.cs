using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        [Key] public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(30, ErrorMessage = "Massimo 30 caratteri")]
        public string Titolo { get; set; }

        public List<Pizza> Pizze { get; set; }

        public Categoria() { }
    }
}
