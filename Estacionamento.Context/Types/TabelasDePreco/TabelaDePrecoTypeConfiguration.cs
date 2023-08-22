using Estacionamento.Domain.TabelasDePreco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Context.Types.TabelasDePreco;

internal class TabelaDePrecoTypeConfiguration : IEntityTypeConfiguration<TabelaDePreco>
{
    public void Configure(EntityTypeBuilder<TabelaDePreco> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Valor).HasPrecision(5,2);
    }
}
