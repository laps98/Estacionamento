using Estacionamento.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.TabelasDePreco;

public class TabelaDePreco
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Carro obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo nome obrigatório")]
    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public int Periodo { get; set; }
}
