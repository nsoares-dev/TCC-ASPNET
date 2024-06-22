using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinalProject___Nicolas.Models;

namespace FinalProject___Nicolas.Data
{
    public class FinalProject___NicolasContext : IdentityDbContext
    {
        public FinalProject___NicolasContext(DbContextOptions<FinalProject___NicolasContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; } 
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; } = default!;
        public DbSet<CategoriaProduto> CategoriaProduto { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<FinalProject___Nicolas.Models.Vendedor> Vendedor { get; set; } = default!;


    }
}
