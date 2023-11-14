using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.Usuarios;

public interface IGerenciadorDeUsuario
{
    public void Save(Usuario cliente);

    public void Update(Usuario cliente);

    public Usuario Get(int id);

    public void Delete(int id);

    ResponsePagination<Usuario> Buscar(QueryFilter filter);
}

internal class GerenciadorDeUsuario : IGerenciadorDeUsuario
{
    private readonly IEstacionamentoContext _context;

    public GerenciadorDeUsuario(IEstacionamentoContext context)
    {
        _context = context;
    }

    public void Save(Usuario cliente)
    {
        try
        {
            _context.Usuarios.Add(cliente);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public void Update(Usuario cliente)
    {
        if (cliente.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar Cliente");
        }
        else
        {
            _context.Usuarios.Update(cliente);
        }
        _context.SaveChanges();
    }

    public Usuario Get(int id)
    {
        return _context.Usuarios.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var cliente = _context.Usuarios.FirstOrDefault(q => q.Id == id);
        if (cliente == null)
            throw new Exception("Este cliente não existe");

        _context.Usuarios.Remove(cliente);
        _context.SaveChanges();
    }

    public ResponsePagination<Usuario> Buscar(QueryFilter filter)
    {
        var lista = _context.Usuarios.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.Usuarios.Count();

        return new ResponsePagination<Usuario>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }
}
