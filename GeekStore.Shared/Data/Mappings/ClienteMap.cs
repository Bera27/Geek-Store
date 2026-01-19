using GeekStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Shared.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasMaxLength(255);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasColumnName("Telefone")
                .HasColumnType("TEXT")
                .HasMaxLength(13);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("TEXT")
                .HasMaxLength(11);

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnName("DataCadastro")
                .HasColumnType("TEXT")
                .HasDefaultValue("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Cpf)
                .IsUnique();
        }
    }
}
