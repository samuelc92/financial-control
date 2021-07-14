namespace FinancialControl.Api.Utils
{
    public static class CalculateUtils
    {
        public static double CalculaValueOfPercent(double value, double percent) =>
            value * (percent / 100);
    }
}