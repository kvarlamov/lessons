using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit; // для обхода в глубину - true - если посещённая вершина
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }
  
    public class SimpleGraph<T>
    {
        public Vertex<T> [] vertex; // список, хранящий вершины
        public int [,] m_adjacency; // матрица смежности
        public int max_vertex;
        private Stack<Vertex<T>> _stack;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int [size,size];
            vertex = new Vertex<T> [size];
            _stack = new Stack<Vertex<T>>();
        }
	
        public void AddVertex(T value)
        {
            // код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex

            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] != null)
                    continue;
                
                var newVertex = new Vertex<T>(value);
                vertex[i] = newVertex;
                break;
            }
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            // ваш код удаления вершины со всеми её рёбрами
            if (!IsIndexCorrect(v))
                return;

            vertex[v] = null;
            for (int i = 0; i < vertex.Length; i++)
            {
                m_adjacency[v, i] = 0;
            }
            
            for (int j = 0; j < vertex.Length; j++)
            {
                m_adjacency[j, v] = 0;
            }
        }
	
        public bool IsEdge(int v1, int v2)
        {
            // true если есть ребро между вершинами v1 и v2
            return IsIndexCorrect(v1) && IsIndexCorrect(v2) && m_adjacency[v1, v2] == 1;
        }
	
        public void AddEdge(int v1, int v2)
        {
            // добавление ребра между вершинами v1 и v2
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;

            m_adjacency[v1, v2] = 1;
            m_adjacency[v2, v1] = 1;
        }
	
        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;
            
            m_adjacency[v1, v2] = 0;
            m_adjacency[v2, v1] = 0;
        }
        
        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.
            
            var path = new List<Vertex<T>>();
            if (!IsIndexCorrect(VFrom) || !IsIndexCorrect(VTo))
                return path;
            
            // 0. Prepare for search
            ClearDataStructures();
            
            // 1. Выбираем текущую вершину
            var res = GetPath(VFrom, VTo);
            Array.Reverse(res);

            foreach (var el in res)
            {
                path.Add(el);
            }

            return path;
        }

        private Vertex<T>[] GetPath(int X, int VTo)
        {
            // 2. Фиксируем вершину X как посещённую.
            vertex[X].Hit = true;
            // 3. Помещаем вершину X в стек.
            _stack.Push(vertex[X]);

            while (_stack.Count > 0)
            {
                // 4. Ищем среди смежных вершин вершины X целевую вершину VTo. Если она найдена, записываем её в стек и возвращаем сам стек как результат работы
                for (int i = 0; i < max_vertex; i++)
                {
                    if (X != i && m_adjacency[X, i] == 1 && i == VTo)
                    {
                        _stack.Push(vertex[i]);
                        return _stack.ToArray();
                    }
                }

                // Если целевой вершины среди смежных нету, то выбираем среди смежных такую вершину, которая ещё не была посещена. Если такая вершина найдена, делаем её текущей X и переходим к п. 2.
                X = GetNotVisited(X);
                if (X != -1)
                    return GetPath(X, VTo);
            
                _stack.Pop();
                if (_stack.Count == 0)
                    break;

                var newCurrent = _stack.Peek();
                newCurrent.Hit = true;
                X = Array.IndexOf(vertex, newCurrent);
            }
            
            return _stack.ToArray(); // возвращаем пустой
        }

        private int GetNotVisited(int currentV)
        {
            for (int i = 0; i < max_vertex; i++)
            {
                if (m_adjacency[currentV, i] == 1 && !vertex[i].Hit)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Очищаем все дополнительные структуры данных: делаем стек пустым, а все вершины графа отмечаем как непосещённые
        /// </summary>
        private void ClearDataStructures()
        {
            _stack.Clear();
            foreach (var v in vertex)
            {
                v.Hit = false;
            }
        }

        private bool IsIndexCorrect(int i) =>
            i >= 0 && i < vertex.Length;
    }
}