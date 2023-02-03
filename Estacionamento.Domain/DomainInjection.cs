using Estacionamento.Domain.Clientes;
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
    }

    private static void Clientes()
    {
        _service.AddTransient<IClienteApi, ClienteApi>();
    }
}
