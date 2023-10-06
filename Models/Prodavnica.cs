using System.ComponentModel.DataAnnotations;
namespace Models{
    public class Prodavnica{
        [Key]
        public int ID{get; set;}
        public List<Automobil>? Automobili{get; set;}
    }
}