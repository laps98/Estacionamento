using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.Vagas;


public interface IGerenciadorDeVaga
{
    public void Save(Vaga Vaga);

    public void Update(Vaga Vaga);

    public Vaga Get(int id);

    public void Delete(int id);

    ResponsePagination<Vaga> Buscar(QueryFilter filter);
}

internal class GerenciadorDeVaga : IGerenciadorDeVaga
{
    private readonly IEstacionamentoContext _context;

    public GerenciadorDeVaga(IEstacionamentoContext context)
    {
        _context = context;
    }

    public void Save(Vaga Vaga)
    {
        try
        {
            _context.Vagas.Add(Vaga);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public void Update(Vaga Vaga)
    {
        if (Vaga.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar Vaga");
        }
        else
        {
            _context.Vagas.Update(Vaga);
        }
        _context.SaveChanges();
    }

    public Vaga Get(int id)
    {
        return _context.Vagas.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var Vaga = _context.Vagas.FirstOrDefault(q => q.Id == id);
        if (Vaga == null)
            throw new Exception("Este Vaga não existe");

        _context.Vagas.Remove(Vaga);
        _context.SaveChanges();
    }

    public ResponsePagination<Vaga> Buscar(QueryFilter filter)
    {
        var lista = _context.Vagas.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.Vagas.Count();

        return new ResponsePagination<Vaga>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }
}
