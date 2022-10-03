using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static void MatrixTurn(string[] Matrix, int M, int N, int T)
        {
            char[,] m = new char[M, N];
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    m[i, j] = Matrix[i][j];
                }
            }

            for (int i = 0, j = 0; i < M / 2 && j < N / 2; i++,j++)
            {
                Rotate(m, M - i, N - j, T, i, j);
            }
            
            for (int i = 0; i < M; i++)
            {
                string currentLine = string.Empty;
                for (int j = 0; j < N; j++)
                {
                    currentLine = string.Concat(currentLine, m[i,j]);
                }

                Matrix[i] = currentLine;
            }
        }

        private static void Rotate(char[,] matrix, int downBorder, int rightBorder, int T, int verticalStart, int horizontalStart)
        {
            if (T == 0)
                return;

            int i = verticalStart;
            int j = horizontalStart;
            char prev = matrix[i, j];
            char tmp;
            
            //go right
            for (j += 1; j < rightBorder; j++)
            {
                tmp = matrix[i, j];
                matrix[i, j] = prev;
                prev = tmp;
            }

            j = rightBorder - 1;
            //go down
            for (i += 1; i < downBorder; i++)
            {
                tmp = matrix[i, j];
                matrix[i, j] = prev;
                prev = tmp;
            }

            i = downBorder - 1;
            //go left
            for (j-=1; j >= horizontalStart; j--)
            {
                tmp = matrix[i, j];
                matrix[i, j] = prev;
                prev = tmp;
            }

            j = horizontalStart;
            //go up
            for (i-=1; i >= verticalStart; i--)
            {
                tmp = matrix[i, j];
                matrix[i, j] = prev;
                prev = tmp;
            }

            T--;
            Rotate(matrix, downBorder, rightBorder, T, verticalStart, horizontalStart);
        }
    }
}