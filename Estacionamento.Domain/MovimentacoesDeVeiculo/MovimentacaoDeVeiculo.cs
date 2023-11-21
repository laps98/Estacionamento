using Estacionamento.Domain.Enums;
using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.TabelasDePreco;
using Estacionamento.Domain.Usuarios;
using Estacionamento.Domain.Vagas;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.MovimentacoesDeVeiculo;

public class MovimentacaoDeVeiculo
{
    public int Id { get; set; }
    public int IdTabelaDePreco { get; set; }
    public int IdUsuario { get; set; }
    public int? IdVaga { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Placa { get; set; }
    public string? Observacao { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public Fluxo Fluxo { get; set; }
    public decimal Valor { get; set; }
    public Status Status { get; set; }
    public DateTime DataDeEntrada { get; set; }
    public DateTime? DataDeSaida { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<MovimentacaoDeCaixa> MovimentacoesDeCaixa { get; set; } = new HashSet<MovimentacaoDeCaixa>();
    public virtual TabelaDePreco TabelaDePreco { get; set; }
    public virtual Vaga Vaga { get; set; }
}