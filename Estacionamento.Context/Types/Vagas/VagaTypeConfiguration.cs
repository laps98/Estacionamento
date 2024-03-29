﻿using Estacionamento.Domain.Vagas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Context.Types.Vagas;

internal class VagaTypeConfiguration : IEntityTypeConfiguration<Vaga>
{
    public void Configure(EntityTypeBuilder<Vaga> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasOne(q => q.Usuario).WithMany(q => q.Vagas).HasForeignKey(q => q.IdUsuario);
    }
}
