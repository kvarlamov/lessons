using System.Globalization;
using Newtonsoft.Json.Linq;
using Telegram.Bot;

namespace HardWork._01_Cyclomatic_Complexity;

internal sealed class Example1_Fixed
{
    private readonly Dictionary<long, bool> _authorizedUsers = new();
    private readonly IHttpClientFactory _clientFactory;
    private bool _waitCarInfo = false;
    private bool _waitLogin;

    private async Task HangleCarInfo(long chatId, string messageText, ITelegramBotClient botClient,
        CancellationToken cancellationToken)
    {
        var client = _clientFactory.CreateClient();
        
        var parts = messageText.Split(' ');

        var dateString = parts[^1];

        (DateTime startdate, DateTime enddate, long selectedInterval) = HandleDate(dateString);

        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync(
                $"https://localhost:6005/Report?vehicleId={parts[1]}&ReportType=1&Interval={selectedInterval}&StartTime={startdate}&EndTime={enddate}",
                cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var data = JObject.Parse(json);
            var result = GetResult(data);
            await botClient.SendTextMessageAsync(
                chatId,
                result,
                cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await botClient.SendTextMessageAsync(
                chatId,
                "Возникла ошибка при обработке запроса. Повторите попытку позже",
                cancellationToken: cancellationToken);
        }
    }

    private static string GetResult(JObject data)
    {
        var result = data.SelectToken("result")!.ToString().Replace("[", "").Replace("]", "");
        if (string.IsNullOrEmpty(result))
            result = "Для данной даты не найдено результатов, повторите запрос";

        return result;
    }

    private (DateTime startdate, DateTime enddate, int selecredInterval) HandleDate(string dateString)
    {
        DateTime date;
        DateTime startdate;
        DateTime enddate;
        var selectedInterval = 1;
        if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            startdate = date.Date;
            enddate = date.Date.AddDays(1).AddMilliseconds(-1);
            return (startdate, enddate, selectedInterval);
        }

        if (DateTime.TryParseExact(dateString, "MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            startdate = new DateTime(date.Year, date.Month, 1);
            enddate = startdate.AddMonths(1).AddDays(-1);
            selectedInterval = 2;
            return (startdate, enddate, selectedInterval);
        }
        
        throw new ApplicationException("Invalid date format");
    }
}