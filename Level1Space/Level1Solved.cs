namespace Level1Space
{
    public class Level1Solved
    {
        public static int squirrel(int N)
        {
            long result = 1;

            for (int i = 2; i <= N; i++)
            {
                result = result * i;
            }

            while (result >= 10)
            {
                result = result / 10;
            }
            
            return (int)result ;
        }
    }
}