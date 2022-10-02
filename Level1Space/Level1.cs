using System;
using System.Collections.Generic;

namespace Level1Space
{
    public static class Level1
    {
        public static string [] TreeOfLife(int H, int W, int N, string [] tree)
        {
            bool isEven = true;
            int yearCounter = 0;
            char[,] resTree = new char[H, W];
            
            // fill the tree
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    var c = tree[i][j] == '+' ? '1' : tree[i][j];
                    resTree[i, j] = c;
                }
            }
            
            while (N > 0)
            {
                bool shouldDestroy = false;
                // increase
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        // adding 48 because '0' has 48 code
                        char c = resTree[i, j] == '.' ? '1' : (char) (char.GetNumericValue(resTree[i, j]) + 1 + 48);
                        resTree[i, j] = c;
                        
                        if (char.GetNumericValue(c) >= 3)
                            shouldDestroy = true;
                    }
                }

                if (shouldDestroy && !isEven)
                {
                    ClearTree(resTree, H, W);
                }

                isEven = !isEven;
                N--;
            }

            for (int i = 0; i < H; i++)
            {
                string currentLine = string.Empty;
                for (int j = 0; j < W; j++)
                {
                    var c = resTree[i, j] == '.' ? '.' : '+';
                    currentLine = string.Concat(currentLine, c);
                }

                tree[i] = currentLine;
            }

            return tree;
        }

        private static void ClearTree(char[,] tree, int H, int W)
        {
            bool[,] mask = new bool[H, W];

            // fill mask for clearing
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (char.GetNumericValue(tree[i, j]) < 3) continue;
                    
                    mask[i, j] = true;
                    // left
                    if (j > 0)
                    {
                        mask[i, j - 1] = true;
                    }
                    // right
                    if (j < W - 1)
                    {
                        mask[i, j + 1] = true;
                    }
                    // up
                    if (i > 0)
                    {
                        mask[i - 1, j] = true;
                    }
                    // down
                    if (i < H - 1)
                    {
                        mask[i + 1, j] = true;
                    }
                }
            }
            
            // clearing
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (mask[i, j])
                        tree[i, j] = '.';
                }
            }
        }
    }
}