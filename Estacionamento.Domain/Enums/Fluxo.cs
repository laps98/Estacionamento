using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Enums;

public enum Fluxo
{
    [Display(Name = "Entrada")]
    Entrada,
    [Display(Name = "Saída")]
    Saida
}
