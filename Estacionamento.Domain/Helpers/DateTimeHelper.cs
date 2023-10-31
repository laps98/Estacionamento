namespace Estacionamento.Doamin.Helpers;

public static class DateTimeHelper
{
    public static DateTime Atual(this DateTime data)
    {
        //var data = DateTime.Now;
        var dia = data.Day;
        var mes = data.Month;
        var ano = data.Year;
        var hora = data.Hour;
        var minuto = data.Minute;
        return new DateTime(ano, mes, dia, hora, minuto, 0);
    }

    public static DateTime HorarioDeBrasilia(this DateTime data)
    {
        return TimeZoneInfo.ConvertTime(data, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
