using Estacionamento.Context;
using Estacionamento.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Estacionamento.Domain.Context;

public class EstacionamentoContext : DbContext, IEstacionamentoContext
{
    public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.RemovePluralizingTableNameConvention();
    }

    public override int SaveChanges()
    {
        UpdateSoftDelete();
        return base.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        UpdateSoftDelete();
        return await base.SaveChangesAsync();
    }

    private void UpdateSoftDelete()
    {
        ChangeTracker.DetectChanges();

        var markedAsDeleted = ChangeTracker.Entries().Where(q => q.State == EntityState.Deleted);

        foreach (var item in markedAsDeleted)
        {
            if (item.Entity is not IExclusaoLogica entity) continue;

            item.State = EntityState.Unchanged;
            //entity.Excluido = true;
        }
    }
}
