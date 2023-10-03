using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.MovimentacoesDeCaixa;

public interface IGerenciadorDeMovimentacaoDeCaixa
{
    void Save(MovimentacaoDeCaixa movimentacao);
    void Update(MovimentacaoDeCaixa movimentacao);
    MovimentacaoDeCaixa Get(int id);
    void Delete(int id);
    ResponsePagination<MovimentacaoDeCaixa> Buscar(QueryFilter filter);
}
internal class GerenciadorDeMovimentacaoDeCaixa : IGerenciadorDeMovimentacaoDeCaixa
{
    private readonly IEstacionamentoContext _context;
    public GerenciadorDeMovimentacaoDeCaixa(IEstacionamentoContext context) => _context = context;

    public void Save(MovimentacaoDeCaixa movimentacao)
    {
        try
        {
            _context.MovimentacoesDeCaixa.Add(movimentacao);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception("Erro inesperado ao cadastrar movimentação");
        }
    }

    public void Update(MovimentacaoDeCaixa movimentacao)
    {
        if (movimentacao.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar movimentação");
        }
        else
        {
            _context.MovimentacoesDeCaixa.Update(movimentacao);
        }
        _context.SaveChanges();
    }

    public MovimentacaoDeCaixa Get(int id)
    {
        return _context.MovimentacoesDeCaixa.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var movimentacao = _context.MovimentacoesDeCaixa.FirstOrDefault(q => q.Id == id);
        if (movimentacao == null)
            throw new Exception("Esta movimentação não existe");
        _context.MovimentacoesDeCaixa.Remove(movimentacao);
        _context.SaveChanges();
    }

    public ResponsePagination<MovimentacaoDeCaixa> Buscar(QueryFilter filter)
    {
        var lista = _context.MovimentacoesDeCaixa.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.MovimentacoesDeCaixa.Count();

        return new ResponsePagination<MovimentacaoDeCaixa>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }
}