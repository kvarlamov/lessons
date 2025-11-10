namespace Game3inRow.Domain
{
    public abstract class BaseElement
    {
        // Получает новый случайный элемент (через element elementFactory)
        public abstract Element GetNext();
    }

    public class Element
    {
    }
}