using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.MovimentacoesDeVeiculo;
public interface IGerenciadorDeMovimentacaoDeVeiculo
{
    void Save(MovimentacaoDeVeiculo movimentacao);
    void Update(MovimentacaoDeVeiculo movimentacao);
    MovimentacaoDeVeiculo Get(int id);
    void Delete(int id);
    ResponsePagination<MovimentacaoDeVeiculo> Buscar(QueryFilter filter);
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
        if (movimentacao.Id == 0)
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
}
