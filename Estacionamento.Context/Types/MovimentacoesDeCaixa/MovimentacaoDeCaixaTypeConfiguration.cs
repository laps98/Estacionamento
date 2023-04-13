using Estacionamento.Domain.MovimentacoesDeCaixa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Context.Types.MovimentacoesDeCaixa;

internal class MovimentacaoDeCaixaTypeConfiguration : IEntityTypeConfiguration<MovimentacaoDeCaixa>
{
    public void Configure(EntityTypeBuilder<MovimentacaoDeCaixa> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.IdMovimentacaoDeVeiculo).IsRequired(false);
        builder.Property(q => q.IdTabelaDePerco).IsRequired(false);

        builder.HasOne(q => q.MovimentacaoDeVeiculo).WithMany(q => q.MovimentacoesDeCaixa).HasForeignKey(q => q.IdMovimentacaoDeVeiculo);
    }
}
