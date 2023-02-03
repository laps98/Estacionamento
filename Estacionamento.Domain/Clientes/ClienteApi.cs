using Estacionamento.Domain.Context;

namespace Estacionamento.Domain.Clientes;

public interface IClienteApi
{
    public List<Cliente> List();

    public void Save(Cliente cliente);

    public Cliente Get(int id);

    public void Delete(int id);

}

internal class ClienteApi : IClienteApi
{
    private readonly IEstacionamentoContext _context;

    public ClienteApi(IEstacionamentoContext context)
    {
        _context = context;
    }

    public List<Cliente> List()
    {
        return _context.Clientes.ToList();
    }

    public void Save(Cliente cliente)
    {
        if (cliente.Id == 0)
        {
            _context.Clientes.Add(cliente);
        }
        else
        {
            _context.Clientes.Update(cliente);
        }
        _context.SaveChanges();
    }

    public Cliente Get(int id)
    {
        return _context.Clientes.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(q => q.Id == id);
        if (cliente == null)
            throw new Exception("Este cliente não existe");

        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }
}
