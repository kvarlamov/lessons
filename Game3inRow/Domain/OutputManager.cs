namespace Game3inRow.Domain
{
    public abstract class OutputManager // -- переделать на GameController - объединив с input manager
    {
        // Возвращает отображение сетки игроку
        // Предусловие - игра была начата (сетка инициализирована)
        public GameGrid GetGrid();

        // Возвращает статистику игрока
        public GameStatistic GetStatistic();
    }
}