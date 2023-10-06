using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Models{
    public class Model{
        [Key]
        public int ID{get; set;}
        public required string NazivModela{get; set;}
        [JsonIgnore]
        public List<Automobil>? Automobili{get; set;}
    }
}