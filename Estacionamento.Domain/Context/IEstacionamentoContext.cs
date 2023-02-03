using Estacionamento.Domain.Clientes;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Domain.Context;

public interface IEstacionamentoContext
{
    public DbSet<Cliente> Clientes { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
