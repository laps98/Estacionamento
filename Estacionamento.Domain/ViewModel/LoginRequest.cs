namespace Estacionamento.Domain.ViewModel;

public sealed record LoginRequest
{
    public string Login { get; set; }
    public string Senha { get; set; }
}