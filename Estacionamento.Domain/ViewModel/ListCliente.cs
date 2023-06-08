using Estacionamento.Domain.Clientes;

namespace Estacionamento.Domain.ViewModel;

public class ListCliente
{
    public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    public int Contador { get; set; }
}
