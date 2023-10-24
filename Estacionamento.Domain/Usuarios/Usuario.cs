using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Usuarios;

public class Usuario
{
    public int Id{ get; set;}

    //[Required(ErrorMessage = "Campo Carro obrigatório")]
    //public string? IdVeiculo { get; set; }

    [Required(ErrorMessage = "Campo nome obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Campo E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail informado não é válido")]
    public string Email { get; set; }
    public string Senha { get; set; }
    public bool Administrador { get; set; }

    //[Required(ErrorMessage = "Campo cep obrigatório")]
    //public string Cep { get; set; }
    //[Required(ErrorMessage = "Campo logrdouro obrigatório")]
    //public string Logradouro { get; set; }
    //[Required(ErrorMessage = "Campo número obrigatório")]
    //public string Numero { get; set; }
    //[Required(ErrorMessage = "Campo bairro obrigatório")]
    //public string Bairro { get; set; }
    //[Required(ErrorMessage = "Campo cidade obrigatório")]
    //public string Cidade { get; set; }
    //[Required(ErrorMessage = "Campo uf obrigatório")]
    //public string Uf { get; set; }
    //public string? Complemento { get; set; }
}
