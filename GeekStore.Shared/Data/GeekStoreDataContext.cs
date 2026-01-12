using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GeekStore.Shared.Data
{
    public class GeekStoreDataContext : DbContext  
    {
        private readonly string _databasePath;

        public GeekStoreDataContext(string databasePath)
            => _databasePath = databasePath;

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            Batteries_V2.Init();
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mappings
        }
    }
}
