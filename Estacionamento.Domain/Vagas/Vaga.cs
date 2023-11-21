using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.Usuarios;

namespace Estacionamento.Domain.Vagas;

public class Vaga
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string Nome { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; } = new HashSet<MovimentacaoDeVeiculo>();
}
