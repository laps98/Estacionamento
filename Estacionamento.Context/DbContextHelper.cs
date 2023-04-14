using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Context;

public static class DbContextHelper
{

    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.ClrType.Name.Contains("Dictionary"))
                continue;

            var tableName = entity.DisplayName();
            entity.SetTableName(tableName);
        }
    }

}
