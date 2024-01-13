namespace OOAP;

public abstract class LinkedList<T>
{
    public const int HEAD_NIL = 0; // put right/left() ещё не вызывалась
    public const int HEAD_OK = 1;  // курсор установили на первый узел
    public const int HEAD_ERR = 2; // список пуст
    
    public const int TAIL_NIL = 0; // put right/left() ещё не вызывалась
    public const int TAIL_OK = 1;  // курсор установили на последний узел
    public const int TAIL_ERR = 2; // список пуст
    
    public const int RIGHT_OK = 1;  // курсор успешно сдвинут
    public const int RIGHT_ERR_EMPTY = 2; // список пуст
    public const int RIGHT_ERR_MINLEN = 3; // в списке менее 2 элементов
    public const int RIGHT_ERR_LAST = 4; // курсор установлен на последний элемент

    public const int REMOVE_OK = 1; // текущий узел успешно удален и курсор сдвинут
    public const int REMOVE_ERR = 2; // список пуст

    public const int REPLACE_OK = 1; // значение узла изменено
    public const int REPLACE_ERR = 2; // список пуст

    public const int FIND_OK = 1; // курсор сдвинут на узел с переданным значением
    public const int FIND_ERR_EMPTY = 2; // список пуст
    public const int FIND_ERR_NOTFOUND = 3; // переданное значение не найдено

    public const int REMOVEALL_OK = 1; // удалены все переданные значения
    public const int REMOVEALL_ERR = 2; // список пуст

    #region commands

    /// <summary>
    /// установить курсор на первый узел в списке <br/>
    /// </summary>
    /// предусловие: список не пустой <br/>
    /// постусловие: курсор установлен на первый узел в списке
    public abstract void Head();
    
    /// <summary>
    /// установить курсор на последний узел в списке <br/>
    /// </summary>
    /// предусловие: список не пустой <br/>
    /// постусловие: курсор установлен на первый узел в списке
    public abstract void Tail();

    /// <summary>
    /// сдвинуть курсор на один узел вправо
    /// </summary>
    /// предусловие: список не пустой, в списке > 1 элемента, курсор не установлен на конец списка <br/>
    /// постусловие: курсор сдвинут на один узел вправо
    public abstract void Right();

    /// <summary>
    /// вставить следом за текущим узлом новый узел с заданным значением <br/>
    /// </summary>
    /// постусловие: добавлен новый узел следом за текущим или первым (если список пустой)
    public abstract void PutRight(T value);

    /// <summary>
    /// вставить перед текущим узлом новый узел с заданным значением <br/>
    /// </summary>
    /// постусловие: добавлен новый узел перед текущим или первым (если список пустой)
    public abstract void PutLeft(T value);

    /// <summary>
    /// удалить текущий узел
    /// (курсор смещается к правому соседу, если он есть, в противном случае курсор смещается к левому соседу, если он есть) <br/>
    /// </summary>
    /// предусловие: список не пустой <br/>
    /// постусловие: удален текущий узел, курсор переместился
    public abstract void Remove();

    /// <summary>
    /// очистить список <br/>
    /// </summary>
    /// постусловие: список очищен, курсор никуда не указывает
    public abstract void Clear();

    /// <summary>
    /// добавить новый узел в хвост списка <br/>
    /// </summary>
    /// постусловие: новый узел добавлен в хвост списка
    public abstract void AddTail(T value);

    /// <summary>
    /// заменить значение текущего узла на заданное <br/>
    /// </summary>
    /// предусловие: список не пустой <br/>
    /// постусловие: значение текущего узла изменено на заданное
    public abstract void Replace(T value);

    /// <summary>
    /// установить курсор на следующий узел с искомым значением (по отношению к текущему узлу) <br/>
    /// </summary>
    /// предусловие: список не пустой, существует еще один узел с искомым значением <br/>
    /// постусловие: курсор установлен на узел с искомым значением
    public abstract void Find(T value);

    /// <summary>
    /// удалить в списке все узлы с заданным значением <br/>
    /// </summary>
    /// предусловие: список не пустой <br/>
    /// постусловие: из списка удалены все узлы с заданным значением
    public abstract void RemoveAll(T value);

    #endregion

    #region queries

    /// <summary>
    /// получить значение текущего узла
    /// </summary>
    /// предусловие: список не пустой <br/>
    public abstract T Get();

    /// <summary>
    /// посчитать количество узлов в списке
    /// </summary>
    public abstract int Size();

    /// <summary>
    /// находится ли курсор в начале списка?
    /// </summary>
    /// предусловие: список не пустой <br/>
    public abstract bool IsHead();

    /// <summary>
    /// находится ли курсор в конце списка?
    /// </summary>
    /// предусловие: список не пустой <br/>
    public abstract bool IsTail();

    /// <summary>
    /// установлен ли курсор на какой-либо узел в списке (по сути, непустой ли список)
    /// </summary>
    public abstract bool IsValue();

    #endregion
}