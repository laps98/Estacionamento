using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Clientes;

public class Cliente
{
    public int Id{ get; set; }
    [Required(ErrorMessage = "Campo nome obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Campo E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail informado não é válido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Campo Carro obrigatório")]
    public string Carro { get; set; }
}
