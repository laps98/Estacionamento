namespace Estacionamento.Web.Helpers;

public static class DateTimeHelper
{
    public static DateTime DataAtual() {
        var data = DateTime.Now;
        var dia = data.Day;
        var mes = data.Month;
        var ano = data.Year;
        var hora = data.Hour;
        var minuto = data.Minute;
        return new DateTime(dia,mes,ano,hora,minuto,00);
    }
}
