using Estacionamento.Domain.Context;
using Estacionamento.Domain.Enums;
using Estacionamento.Domain.Pagination;
using System.ComponentModel.DataAnnotations;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.MovimentacoesDeVeiculo;
public interface IGerenciadorDeMovimentacaoDeVeiculo
{
    void Save(MovimentacaoDeVeiculo movimentacao);
    void Update(MovimentacaoDeVeiculo movimentacao);
    MovimentacaoDeVeiculo Get(int id);
    void Delete(int id);
    ResponsePagination<MovimentacaoDeVeiculo> Buscar(QueryFilter filter);
    MovimentacaoDeVeiculo Calcular(string placa);
}
internal class GerenciadorDeMovimentacaoDeVeiculo : IGerenciadorDeMovimentacaoDeVeiculo
{
    private readonly IEstacionamentoContext _context;
    public GerenciadorDeMovimentacaoDeVeiculo(IEstacionamentoContext context) => _context = context;

    public void Save(MovimentacaoDeVeiculo movimentacao)
    {
        try
        {
            _context.MovimentacoesDeVeiculo.Add(movimentacao);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception("Erro inesperado ao cadastrar movimentação");
        }
    }
    public void Update(MovimentacaoDeVeiculo movimentacao)
    {
        if (movimentacao == null ||movimentacao.Id == 0 )
        {
            throw new Exception("Erro inesperado ao atualizar movimentação");
        }
        else
        {
            _context.MovimentacoesDeVeiculo.Update(movimentacao);
        }
        _context.SaveChanges();
    }
    public MovimentacaoDeVeiculo Get(int id)
    {
        return _context.MovimentacoesDeVeiculo.First(q => q.Id == id);
    }
    public void Delete(int id)
    {
        var movimentacao = _context.MovimentacoesDeVeiculo.FirstOrDefault(q => q.Id == id);
        if (movimentacao == null)
            throw new Exception("Esta movimentação não existe");
        _context.MovimentacoesDeVeiculo.Remove(movimentacao);
        _context.SaveChanges();
    }
    public ResponsePagination<MovimentacaoDeVeiculo> Buscar(QueryFilter filter)
    {
        var lista = _context.MovimentacoesDeVeiculo.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.MovimentacoesDeVeiculo.Count();

        return new ResponsePagination<MovimentacaoDeVeiculo>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }

    public MovimentacaoDeVeiculo Calcular(string placa)
    {
        var hoje = DateTime.Now;
        var movimentacao = _context.MovimentacoesDeVeiculo.FirstOrDefault(q => q.Placa == placa &&
                                                                               q.DataDeEntrada.Date == hoje.Date &&
                                                                               q.DataDeSaida == null);
        if (movimentacao == null)
            throw new Exception("Não foi encontrado nenhum veículo");
        var tabela = _context.TabelasDePreco.First(q => q.Id == movimentacao.IdTabelaDePreco);

        var valorAPAgar = (decimal)(hoje - movimentacao.DataDeEntrada).TotalMinutes / tabela.Periodo * tabela.Valor;

        movimentacao.DataDeSaida = hoje;
        movimentacao.Valor = valorAPAgar;

        return new MovimentacaoDeVeiculo
        {
            Id = movimentacao.Id,
            IdTabelaDePreco = movimentacao.IdTabelaDePreco,
            Placa = movimentacao.Placa,
            Observacao = movimentacao.Observacao,
            Fluxo = movimentacao.Fluxo,
            Valor = movimentacao.Valor,
            DataDeEntrada = movimentacao.DataDeEntrada,
            DataDeSaida = movimentacao.DataDeSaida,
        };
    }
    public void Baixar(MovimentacaoDeVeiculo movimentacao)
    {
        var hoje = new DateTime();
        //var movimentacaoD = _context.MovimentacoesDeVeiculo.FirstOrDefault(q => q.Id == id &&
        //                                                                       q.DataDeEntrada.Date == hoje.Date &&
        //                                                                       q.DataDeSaida == null);
        if (movimentacao == null)
            throw new Exception("Não foi encontrado nenhum veículo");
        var tabela = _context.TabelasDePreco.First(q => q.Id == movimentacao.IdTabelaDePreco);

        var valorAPAgar = (decimal)(hoje - movimentacao.DataDeEntrada).TotalMinutes / tabela.Periodo * tabela.Valor;

        movimentacao.DataDeSaida = hoje;
        movimentacao.Valor = valorAPAgar;

        _context.SaveChanges();
    }
}
