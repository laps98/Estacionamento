using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.TabelasDePreco;
using Estacionamento.Domain.Vagas;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Domain.Usuarios;

public class Usuario
{
    public int Id{ get; set;}
    [Required(ErrorMessage = "Campo nome obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Campo E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail informado não é válido")]
    public string Email { get; set; }
    public string Senha { get; set; }
    public bool Administrador { get; set; }

    public virtual ICollection<Vaga> Vagas { get; set; } = new HashSet<Vaga>();
    public virtual ICollection<TabelaDePreco> TabelasDePreco { get; set; } = new HashSet<TabelaDePreco>();
    public virtual ICollection<MovimentacaoDeCaixa> MovimentacoesDeCaixa { get; set; } = new HashSet<MovimentacaoDeCaixa>();
    public virtual ICollection<MovimentacaoDeVeiculo> MovimentacoesDeVeiculo { get; set; } = new HashSet<MovimentacaoDeVeiculo>();

}
