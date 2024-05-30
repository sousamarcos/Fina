
namespace Fina.Core.Ferramentas
{
    public static class DateTimeExtension
    {
        public static DateTime GetPrimeiroDia(this DateTime data, int? ano = null, int? mes = null)
        {
            return new DateTime(ano ?? data.Year, mes ?? data.Month, 1);
        }

        public static DateTime GetUltimoDia(this DateTime data, int? ano = null, int? mes = null)
        {
            return new DateTime(ano ?? data.Year, mes ?? data.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
