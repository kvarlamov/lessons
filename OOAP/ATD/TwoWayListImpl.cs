namespace OOAP;

public class TwoWayListImpl<T> : ParentListImpl<T>
{
    private int _left_status;
    
    public void Left()
    {
        if (_cursor is null)
        {
            _left_status = RIGHT_ERR_EMPTY;
            return;
        }
        
        if (_cursor.left is null)
        {
            _left_status = RIGHT_ERR_NORIGHT;
        }
        else
        {
            _cursor = _cursor.left;
            _left_status = LEFT_OK;
        }
    } 
}