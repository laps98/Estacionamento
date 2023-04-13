using Estacionamento.Domain.TabelasDePreco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Context.Types.TabelasDePreco;

internal class TabelaDePrecoTypeConfiguration : IEntityTypeConfiguration<TabelaDePreco>
{
    public void Configure(EntityTypeBuilder<TabelaDePreco> builder)
    {
        throw new NotImplementedException();
    }
}
