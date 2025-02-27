namespace Game3inRow.Domain
{
    public abstract class InputManager 
    {
        // todo - добавить статусы-состояния

        // Команды:

        // Сделать ход (Переставить элементы) - отправляет команду в GameManager - logic    
        // Предусловие - получен корректный ввод (правильное количество координат)
        public abstract void Turn(string input); // todo - результат хода - перерисованная сетка

        // Начать новую игру
        public abstract void StartNewGame(); // результат - сетка

        // Завершить игру
        // Предусловие - игра была начата
        public abstract void EndGame(); // результат - статистика
    }
}