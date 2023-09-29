using Estacionamento.Domain.Clientes;
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
        _service.AddTransient<IGerenciadorDeTabelaDePreco, GerenciadorDeTabelaDePreco>();
    }

    private static void Clientes()
    {
        _service.AddTransient<IClienteRepository, ClienteRepository>();
    }
}
