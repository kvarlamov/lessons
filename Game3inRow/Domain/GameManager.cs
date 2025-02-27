namespace Game3inRow.Domain
{
    public abstract class GameManager()
    {
        // todo - добавить состояния игры

        // КОМАНДЫ:

        // Начинает новую игру
        // Предусловие - игра не начата
        public abstract void InitializeNewGame();

        // Переставить элементы сетки
        // Предусловие - элементы находятся рядом и их можно Переставить
        // Постусловие - элементы переставлены, записана статистика
        public abstract void ProcessTurn();

        // Завершает игру и оповещает все необходимые компоненты
        // Предусловие - игра была начата
        public abstract void FinishGame();

    
        // ЗАПРОСЫ:
        // Проверяет возможность ходов - есть ли комбинации для удаления на поле
        public abstract bool CheckPossibleMoves();

        // Проверка наличия 3 в ряд
        public abstract bool CheckCombinations();
    }
}