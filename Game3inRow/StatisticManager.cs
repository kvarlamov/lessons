using Game3inRow.Domain;

public abstract class StatisticManager
{
    // Обновить статистику игрока
    public abstract void UpdateStatistic(string turn); //todo - возможно лучше переделать на структуру

    // Возвращает статистику игрока
    public abstract GameStatistic GetStatisticForUser();
}