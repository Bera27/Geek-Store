using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    internal class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasMaxLength(255);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasMaxLength(255);

            builder.Property(x => x.PrecoCompra)
                .IsRequired()
                .HasColumnName("PrecoCompra")
                .HasConversion(
                    v => (long)(v * 100m),
                    v => v / 100m)
                .HasColumnType("INTEGER");

            builder.Property(x => x.PrecoVenda)
                .IsRequired()
                .HasColumnName("PrecoVenda")
                .HasConversion(
                    v => (long)(v * 100m),
                    v => v / 100m)
                .HasColumnType("INTEGER");
        }
    }
}
