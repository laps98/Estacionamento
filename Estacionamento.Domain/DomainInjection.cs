using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.TabelasDePreco;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Contracts;

namespace Estacionamento.Domain;

public static class DomainInjection
{
    private static IServiceCollection _service = null!;

    public static void AddDomainServices(this IServiceCollection services)
    {
        _service = services;

        Clientes();
        TabelaDePreco();
        MovimentacaoDeCaixa();
        MovimentacaoDeVeiculo();
    }

    private static void MovimentacaoDeVeiculo()
    {
        _service.AddTransient<IGerenciadorDeMovimentacaoDeVeiculo, GerenciadorDeMovimentacaoDeVeiculo>();
    }

    private static void MovimentacaoDeCaixa()
    {
        _service.AddTransient<IGerenciadorDeMovimentacaoDeCaixa, GerenciadorDeMovimentacaoDeCaixa>();
    }

    private static void TabelaDePreco()
    {
        _service.AddTransient<IGerenciadorDeTabelaDePreco, GerenciadorDeTabelaDePreco>();
    }

    private static void Clientes()
    {
        _service.AddTransient<IGerenciadorDeCliente, GerenciadorDeCliente>();
    }
}
