using System.Globalization;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HardWork._01___Cyclomatic_Complexity;

internal sealed class Example1_WithoutFix
{
    private readonly Dictionary<long, bool> _authorizedUsers = new ();
    private readonly IHttpClientFactory _clientFactory;
    private bool _waitLogin = false;
    private bool _waitCarInfo = false;
    
    private async Task HangleCarInfo(Message message, string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            if (!_authorizedUsers.ContainsKey(chatId))
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "У пользователя отсутствую права данной операции. Авторизуйтесь с помощью команды /login и повторите",
                    cancellationToken: cancellationToken);
                _waitLogin = false;
                return;
            }

            var client = _clientFactory.CreateClient();

            var parts = messageText.Split(' ');
            
            string dateString = parts[^1];

            DateTime date;
            DateTime startdate = default;
            DateTime enddate = default;
            int selectedInterval = 1;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                startdate = date.Date;
                enddate = date.Date.AddDays(1).AddMilliseconds(-1);
            }
            else if (DateTime.TryParseExact(dateString, "MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                startdate = new DateTime(date.Year, date.Month, 1);
                enddate = startdate.AddMonths(1).AddDays(-1);
                selectedInterval = 2;
            }
            else
            {
                Console.WriteLine("Invalid date format");
            }

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"https://localhost:6005/Report?vehicleId={parts[1]}&ReportType=1&Interval={selectedInterval}&StartTime={startdate}&EndTime={enddate}", cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Возникла ошибка при обработке запроса. Повторите попытку позже",
                    cancellationToken: cancellationToken);
                return;
            }
            
            // если в дате есть день - запрашиваем по дню, если месяц - по месяцу
            // Report?vehicleId={1}&ReportType=1&Interval={(int)selectedInterval}&StartTime={startDate.Date}&EndTime={endDate.Date}

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var data = JObject.Parse(json);
            string result = string.Empty;
            try
            {
                result = data.SelectToken("result")!.ToString().Replace("[","").Replace("]","");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (string.IsNullOrEmpty(result))
                result = "Для данной даты не найдено результатов, повторите запрос";

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: result,
                cancellationToken: cancellationToken);

        }
}