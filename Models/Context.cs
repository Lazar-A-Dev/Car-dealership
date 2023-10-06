using Microsoft.EntityFrameworkCore;

namespace Models{
    public class Context:DbContext{
        public DbSet<Prodavnica> Prodavnice{get; set;}
        public DbSet<Automobil> Automobili{get; set;}
        public DbSet<Marka> Marke{get; set;}
        public DbSet<Model> Modeli{get; set;}
        public DbSet<Boja> Boje{get; set;}

        public Context(DbContextOptions options): base(options){
            
        }
    }
}