using Estacionamento.Domain.MovimentacoesDeVeiculo;

namespace Estacionamento.Domain.Vagas;

public class Vaga
{
    public int Id { get; set; }
    public int Nome { get; set; }

    public virtual ICollection<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; } = new HashSet<MovimentacaoDeVeiculo>();
}
