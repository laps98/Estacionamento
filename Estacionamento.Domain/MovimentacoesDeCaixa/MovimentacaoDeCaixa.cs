using Estacionamento.Domain.Enums;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.TabelasDePreco;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.MovimentacoesDeCaixa;

public class MovimentacaoDeCaixa
{
    public int Id { get; set; }

    public int? IdMovimentacaoDeVeiculo { get; set; }

    public int? IdTabelaDePerco { get; set; }

    [Required(ErrorMessage = "Campo Carro obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo nome obrigatório")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Campo cep obrigatório")]
    public Fluxo Fluxo { get; set; }

    public DateTime Data { get; set; }

    public virtual MovimentacaoDeVeiculo? MovimentacaoDeVeiculo { get; set; }

    public virtual TabelaDePreco? TabelaDePreco { get; set; }
}
