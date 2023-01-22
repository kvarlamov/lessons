using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {
        public int [] HeapArray; // хранит неотрицательные числа-ключи

        public int lastPointer; // указатель на свободный слот (если куча заполнена = длине HeapArray)

        public Heap()
        {
            HeapArray = null;
            lastPointer = 0;
        }
		
        public void MakeHeap(int[] a, int depth)
        {
            // создаём массив кучи HeapArray из заданного
            // размер массива выбираем на основе глубины depth
            int size = GetTreeSizeByDepth(depth);
            HeapArray = new int[size];
            foreach (var node in a) 
                Add(node);
        }

        public int GetMax()
        {
            // вернуть значение корня и перестроить кучу
            if (HeapArray == null || HeapArray.Length == 0 || lastPointer == 0)
                return -1; // если куча пуста

            // сохраняем корень перед перемещением
            int max = HeapArray[0];
            // ставим на место корня последний элемент
            HeapArray[0] = HeapArray[lastPointer-1];
            lastPointer--;

            // обновляем новый корень
            MoveDown(0);

            return max;
        }

        public bool Add(int key)
        {
            // добавляем новый элемент key в кучу и перестраиваем её
            if (lastPointer == HeapArray.Length)
                return false; // если куча вся заполнена

            Insert(key);
            lastPointer++;
            return true;
        }

        /// <summary>
        /// Операция просеивания вниз
        /// </summary>
        /// <param name="i"></param>
        private void MoveDown(int i)
        {
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int max = i;
                
                // если левый больше максимума делаем его максимумом
                if (left < lastPointer && HeapArray[left] > HeapArray[max])
                    max = left;
                
                // если правый больше максимума делаем его максимумом
                if (right < lastPointer && HeapArray[right] > HeapArray[max])
                    max = right;
                
                // останавливаемся, когда у родителя будет больший ключ, а у двух наследников -- меньшие.
                if (max == i)
                    break;

                (HeapArray[max], HeapArray[i]) = (HeapArray[i], HeapArray[max]);
                i = max;
            }
        }
        
        private void Insert(int key)
        {
            // находим индекс родителя
            HeapArray[lastPointer] = key;
            int i = lastPointer;
            int parent = (i - 1) / 2;

            while (i > 0 && HeapArray[parent] < HeapArray[i])
            {
                (HeapArray[i], HeapArray[parent]) = (HeapArray[parent], HeapArray[i]);

                i = parent;
                parent = (i - 1) / 2;
            }
        }
        
        private int GetTreeSizeByDepth(int depth)
        {
            if (depth == 0)
                return 1;

            return (int)(Math.Pow(2, depth + 1) - 1);
        }
    }
}