using System.ComponentModel.DataAnnotations;
namespace Models{
    public class Automobil{
        [Key]
        public int ID{get; set;}
        public required int Cena{get; set;}
        public required DateTime PoslednjaProdaja{get; set;}
        public required int Kolicina{get; set;}
        public required string? Slika{get; set;}
        public Marka? Marka{get; set;}
        public Model? Model{get; set;}
        public Boja? Boja{get; set;}
    }
}