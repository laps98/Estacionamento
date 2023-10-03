using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.TabelasDePreco;

public interface IGerenciadorDeTabelaDePreco
{
    void Save(TabelaDePreco tabelaDePreco);
    void Update(TabelaDePreco tabelaDePreco);
    TabelaDePreco Get(int id);
    void Delete(int id);
    ResponsePagination<TabelaDePreco> Buscar(QueryFilter filter);
}
internal class GerenciadorDeTabelaDePreco : IGerenciadorDeTabelaDePreco
{
    private readonly IEstacionamentoContext _context;

    public GerenciadorDeTabelaDePreco(IEstacionamentoContext context)
    {
        _context = context;
    }

    public void Save(TabelaDePreco tabelaDePreco)
    {
        try
        {
            _context.TabelasDePreco.Add(tabelaDePreco);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception("Erro inesperado ao cadastrar Tabela De Preço");
        }
    }

    public void Update(TabelaDePreco tabelaDePreco)
    {
        if (tabelaDePreco.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar Tabela De Preço");
        }
        else
        {
            _context.TabelasDePreco.Update(tabelaDePreco);
        }
        _context.SaveChanges();
    }

    public TabelaDePreco Get(int id)
    {
        return _context.TabelasDePreco.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var tabelaDePreco = _context.TabelasDePreco.FirstOrDefault(q => q.Id == id);
        if (tabelaDePreco == null)
            throw new Exception("Esta tabela De Preço não existe");
        _context.TabelasDePreco.Remove(tabelaDePreco);
        _context.SaveChanges();
    }

    public ResponsePagination<TabelaDePreco> Buscar(QueryFilter filter)
    {
        var lista = _context.TabelasDePreco.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.TabelasDePreco.Count();

        return new ResponsePagination<TabelaDePreco>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }
}
