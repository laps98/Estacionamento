using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.TabelasDePreco;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Domain.Context;

public interface IEstacionamentoContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<MovimentacaoDeCaixa> MovimentacoesDeCaixa { get; set; }
    public DbSet<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; }
    public DbSet<TabelaDePreco> TabelasDePreco { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
