using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Rua)
                .IsRequired()
                .HasColumnName("Rua")
                .HasMaxLength(50);

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasColumnName("Bairro")
                .HasMaxLength(50);

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasColumnName("Numero")
                .HasColumnType("TEXT")
                .HasMaxLength(6);

            builder.Property(x => x.Complemento)
                .IsRequired()
                .HasColumnName("Complemento")
                .HasMaxLength(50);

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
