public abstract class BaseEntity
{
    public int id;
}

public class ElementFactory
{
    // Возвращает элемент
    public BaseElement GetNext(){}
}

public abstract class GameGrid
{
    // todo добавить статусы 
    private int[,] _field = new int[8,8];

    // КОМАНДЫ:
    // Инициализирует игровое поле при начале новой игры
    // Предусловие - новая сетка еще не создавалась
    // Постусловие - создана игровая сетка, заполненная элементами
    public abstract void Initialize();

    // Переставить элементы (по вводу)
    // Предусловие - в игровой сетке есть элементы (сетка создана)
    // Постусловие - элементы переставлены
    public abstract void ChangeElements();

    // Удалить элементы и сдвинуть вниз для заполнения пустот
    // Предусловие - в игровой сетке есть элементы (сетка создана)
    // Постусловие - элементы сдвинуты    
    public abstract void UpdateOnTurn();

    // Заполнить пустоты после удаления
    // Предусловие - кол-во элементов меньше исходного
    // Постусловие - кол-во элементов соответствует исходному
    public abstract void ReDrawField();


    // ЗАПРОСЫ:
    // Получает текущее игровое поле
    // Предусловие: закончено создание поля
    public GameGrid GetGrid();
}

public abstract class BaseElement
{
    // Получает новый случайный элемент (через element elementFactory)
    public abstract Element GetNext();
}

public abstract class GameStatistic : BaseEntity
{
    public string[] Turns {get;};
    public User User {get;};
}

public class User : BaseEntity
{
    public string Name {get;}
}


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

public abstract class OutputManager // -- переделать на GameController - объединив с input manager
{
    // Возвращает отображение сетки игроку
    // Предусловие - игра была начата (сетка инициализирована)
    public GameGrid GetGrid();

    // Возвращает статистику игрока
    public GameStatistic GetStatistic();
}

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

public abstract class StatisticManager
{
    // Обновить статистику игрока
    public abstract void UpdateStatistic(string turn); //todo - возможно лучше переделать на структуру

    // Возвращает статистику игрока
    public abstract GameStatistic GetStatisticForUser();
}