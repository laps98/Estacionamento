using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.TabelasDePreco;

public class TabelaDePreco
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Carro obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo nome obrigatório")]
    public decimal Valor { get; set; }

    public DateTime Data { get; set; } = DateTime.Now.Atual();

    [Display(Name = "Periodo em minutos")]
    public int Periodo { get; set; }

    public virtual ICollection<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; } = new HashSet<MovimentacaoDeVeiculo>();
}
