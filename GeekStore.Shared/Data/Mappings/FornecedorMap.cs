using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Empresa)
                .IsRequired()
                .HasColumnName("Empresa")
                .HasMaxLength(255);

            builder.Property(x => x.NomeVendedor)
                .IsRequired()
                .HasColumnName("NomeVendedor")
                .HasMaxLength(255);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasColumnName("Telefone")
                .HasColumnType("TEXT")
                .HasMaxLength(13);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("TEXT")
                .HasMaxLength(255);
        }
    }
}
