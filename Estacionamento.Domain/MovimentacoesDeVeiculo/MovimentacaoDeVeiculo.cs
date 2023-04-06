using Estacionamento.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.MovimentacoesDeVeiculo;

public class MovimentacaoDeVeiculo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Carro obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo cep obrigatório")]
    public Fluxo Fluxo { get; set; }

    public DateTime DataDeEntrada { get; set; }

    public DateTime DataDeSaida { get; set; }

}
