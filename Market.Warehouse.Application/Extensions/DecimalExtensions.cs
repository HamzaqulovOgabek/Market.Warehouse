namespace Market.Warehouse.Application.Extensions;

public static class DecimalExtensions
{
    public static string FormattedToString(this decimal value)
    {
        if (value == (int)value)
            return ((int)value).ToString();
        else
        {
            var res = value.ToString();
            while (res[res.Length - 1] == '0')
                res = res.Substring(0, res.Length - 1);
            return res;
        }
    }
}
