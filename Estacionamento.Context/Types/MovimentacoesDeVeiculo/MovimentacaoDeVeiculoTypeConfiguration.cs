using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Context.Types.MovimentacoesDeVeiculo;

internal class MovimentacaoDeVeiculoTypeConfiguration : IEntityTypeConfiguration<MovimentacaoDeVeiculo>
{
    public void Configure(EntityTypeBuilder<MovimentacaoDeVeiculo> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasOne(q => q.TabelaDePreco).WithMany(q => q.MovimentacoesDeVeiculo).HasForeignKey(q => q.IdTabelaDePreco);
        builder.HasOne(q => q.Vaga).WithMany(q => q.MovimentacoesDeVeiculo).HasForeignKey(q => q.IdVaga).IsRequired(false);
        builder.HasOne(q => q.Usuario).WithMany(q => q.MovimentacoesDeVeiculo).HasForeignKey(q => q.IdUsuario);

    }
}
