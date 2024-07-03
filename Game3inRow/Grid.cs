public class GameGrid<T>
{
    private readonly T[,] _grid;
    private readonly Dictionary<int, int> _xIndexMap;
    private readonly Dictionary<char, int> _yIndexMap;

    public GameGrid(int xSize = 8, int ySize = 8)
    {
        _grid = new T[xSize, ySize];
        
        _xIndexMap = new Dictionary<int, int>();
        for (int i = 0; i < xSize; i++)
        {
            _xIndexMap[i + 1] = i;
        }

        _yIndexMap = new Dictionary<char, int>();
        char yStart = 'A';
        for (int i = 0; i < ySize; i++)
        {
            _yIndexMap[(char)(yStart + i)] = i;
        }
    }

    public T this[int x, char y]
    {
        get
        {
            if (!_xIndexMap.ContainsKey(x))
                throw new IndexOutOfRangeException($"Invalid x index: {x}");
            if (!_yIndexMap.ContainsKey(y))
                throw new IndexOutOfRangeException($"Invalid y index: {y}");
                
            return _grid[_xIndexMap[x], _yIndexMap[y]];
        }
        set
        {
            if (!_xIndexMap.ContainsKey(x))
                throw new IndexOutOfRangeException($"Invalid x index: {x}");
            if (!_yIndexMap.ContainsKey(y))
                throw new IndexOutOfRangeException($"Invalid y index: {y}");
                
            _grid[_xIndexMap[x], _yIndexMap[y]] = value;
        }
    }

    public void PrintGameField()
    {
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                Console.Write($"{_grid[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}