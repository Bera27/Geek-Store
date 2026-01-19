using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ValorTotal)
                .IsRequired()
                .HasColumnName("ValorTotal")
                .HasConversion(
                    v => (long)(v * 100m),
                    v => v / 100m)
                .HasColumnType("INTEGER");

            builder.HasOne(x => x.Funcionario)
                .WithMany(f => f.Vendas)
                .HasForeignKey(x => x.IdFuncionario)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.DataVenda)
                .IsRequired()
                .HasColumnName("DataVenda")
                .HasColumnType("TEXT")
                .HasDefaultValue("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }
    }
}
