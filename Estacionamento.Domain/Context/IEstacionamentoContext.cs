using Estacionamento.Domain.Usuarios;
using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.TabelasDePreco;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Domain.Vagas;

namespace Estacionamento.Domain.Context;

public interface IEstacionamentoContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<MovimentacaoDeCaixa> MovimentacoesDeCaixa { get; set; }
    public DbSet<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; }
    public DbSet<TabelaDePreco> TabelasDePreco { get; set; }
    public DbSet<Vaga> Vagas { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
