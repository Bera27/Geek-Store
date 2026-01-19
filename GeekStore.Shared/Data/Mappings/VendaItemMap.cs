using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    public class VendaItemMap : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnName("Quantidade")
                .HasColumnType("INTEGER");

            builder.Property(x => x.PrecoUN)
                .IsRequired()
                .HasColumnName("PrecoUN")
                .HasConversion(
                    v => (long)(v * 100m),
                    v => v / 100m)
                .HasColumnType("INTEGER");

            builder.HasOne(x => x.Venda)
                .WithMany(x => x.VendaItens)
                .HasForeignKey(x => x.IdVenda)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.VendaItens)
                .HasForeignKey(x => x.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
