using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Models{
    public class Marka{
        [Key]
        public int ID{get; set;}
        public required string NazivMarke{get; set;}
        
        [JsonIgnore]
        public List<Automobil>? Automobili{get; set;}
    }
}