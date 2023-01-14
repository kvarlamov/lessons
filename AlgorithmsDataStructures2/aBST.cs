using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class aBST
    {
        public int? [] Tree; // массив ключей
	
        public aBST(int depth)
        {
            int tree_size = GetTreeSizeByDepth(depth);
            Tree = new int?[tree_size];
            for(int i=0; i<tree_size; i++) 
                Tree[i] = null;
        }
	
        public int? FindKeyIndex(int key)
        {
            // ищем в массиве индекс ключа
            return FindKeyIndex(0, key);
        }
	
        public int AddKey(int key)
        {
            // добавляем ключ в массив
            // индекс добавленного/существующего ключа или -1 если не удалось
            var findResult = FindKeyIndex(key);
            
            if (findResult == null)
                return -1;
            
            if (findResult >= 0)
                return findResult.Value;

            int newIndex = Math.Abs(findResult.Value); 
            Tree[newIndex] = key;
            return newIndex;
        }

        private int GetTreeSizeByDepth(int depth)
        {
            if (depth == 0)
                return 1;

            return (int)(Math.Pow(2, depth + 1) - 1);
        }

        private int? FindKeyIndex(int currentIndex, int key)
        {
            if (currentIndex > Tree.Length - 1)
                return null;

            var currentKey = Tree[currentIndex];
            if (currentKey == key)
                return currentIndex;

            if (currentKey is null)
                return currentIndex * -1;

            if (currentKey > key)
                //ищем в левом поддереве
                return FindKeyIndex(2 * currentIndex + 1, key);
            else
                //ищем в правом поддереве
                return FindKeyIndex(2 * currentIndex + 2, key);
        }
    }
}