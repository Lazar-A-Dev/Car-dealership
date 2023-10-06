using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Models{
    public class Boja{
        [Key]
        public int ID{get; set;}
        public required string NazivBoje{get; set;}
        [JsonIgnore]
        public List<Automobil>? Automobili{get; set;}
    }
}