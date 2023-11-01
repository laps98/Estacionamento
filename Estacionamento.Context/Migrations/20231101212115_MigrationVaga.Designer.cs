﻿// <auto-generated />
using System;
using Estacionamento.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Estacionamento.Context.Migrations
{
    [DbContext(typeof(EstacionamentoContext))]
    [Migration("20231101212115_MigrationVaga")]
    partial class MigrationVaga
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Estacionamento.Domain.MovimentacoesDeCaixa.MovimentacaoDeCaixa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Fluxo")
                        .HasColumnType("int");

                    b.Property<int?>("IdMovimentacaoDeVeiculo")
                        .HasColumnType("int");

                    b.Property<int?>("IdTabelaDePerco")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("IdMovimentacaoDeVeiculo");

                    b.ToTable("MovimentacaoDeCaixa");
                });

            modelBuilder.Entity("Estacionamento.Domain.MovimentacoesDeVeiculo.MovimentacaoDeVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDeEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataDeSaida")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Fluxo")
                        .HasColumnType("int");

                    b.Property<int>("IdTabelaDePreco")
                        .HasColumnType("int");

                    b.Property<int?>("IdVaga")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("IdTabelaDePreco");

                    b.HasIndex("IdVaga");

                    b.ToTable("MovimentacaoDeVeiculo");
                });

            modelBuilder.Entity("Estacionamento.Domain.TabelasDePreco.TabelaDePreco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Periodo")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("TabelaDePreco");
                });

            modelBuilder.Entity("Estacionamento.Domain.Usuarios.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Administrador")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Estacionamento.Domain.Vagas.Vaga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Nome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vaga");
                });

            modelBuilder.Entity("Estacionamento.Domain.MovimentacoesDeCaixa.MovimentacaoDeCaixa", b =>
                {
                    b.HasOne("Estacionamento.Domain.MovimentacoesDeVeiculo.MovimentacaoDeVeiculo", "MovimentacaoDeVeiculo")
                        .WithMany("MovimentacoesDeCaixa")
                        .HasForeignKey("IdMovimentacaoDeVeiculo");

                    b.Navigation("MovimentacaoDeVeiculo");
                });

            modelBuilder.Entity("Estacionamento.Domain.MovimentacoesDeVeiculo.MovimentacaoDeVeiculo", b =>
                {
                    b.HasOne("Estacionamento.Domain.TabelasDePreco.TabelaDePreco", "TabelaDePreco")
                        .WithMany("MovimentacoesDeVeiculo")
                        .HasForeignKey("IdTabelaDePreco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Estacionamento.Domain.Vagas.Vaga", "Vaga")
                        .WithMany("MovimentacoesDeVeiculo")
                        .HasForeignKey("IdVaga");

                    b.Navigation("TabelaDePreco");

                    b.Navigation("Vaga");
                });

            modelBuilder.Entity("Estacionamento.Domain.MovimentacoesDeVeiculo.MovimentacaoDeVeiculo", b =>
                {
                    b.Navigation("MovimentacoesDeCaixa");
                });

            modelBuilder.Entity("Estacionamento.Domain.TabelasDePreco.TabelaDePreco", b =>
                {
                    b.Navigation("MovimentacoesDeVeiculo");
                });

            modelBuilder.Entity("Estacionamento.Domain.Vagas.Vaga", b =>
                {
                    b.Navigation("MovimentacoesDeVeiculo");
                });
#pragma warning restore 612, 618
        }
    }
}
