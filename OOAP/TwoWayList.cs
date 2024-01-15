namespace OOAP;

public abstract class TwoWayList<T> : ParentList<T>
{
    /// <summary>
    /// сдвинуть курсор на один узел влево
    /// </summary>
    /// предусловие: левее курсора есть элемент;
    /// постусловие: курсор сдвинут на один узел влево
    public abstract void Left();
}