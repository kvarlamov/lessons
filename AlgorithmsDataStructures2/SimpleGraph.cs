using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex
    {
        public int Value;
        public Vertex(int val)
        {
            Value = val;
        }
    }
  
    public class SimpleGraph
    {
        public Vertex [] vertex;
        public int [,] m_adjacency; // матрица смежности
        public int max_vertex; // список, хранящий вершины (Индексы соответствуют индексам матрицы)
	
        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int [size,size];
            vertex = new Vertex [size];
        }
	
        public void AddVertex(int value)
        {
            // код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex

            for (int i = 0; i < vertex.Length; i++)
            {
                if (vertex[i] != null)
                    continue;
                
                var newVertex = new Vertex(value);
                vertex[i] = newVertex;
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
        }
	
        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;
            
            m_adjacency[v1, v2] = 0;
        }

        private bool IsIndexCorrect(int i) =>
            i >= 0 && i < vertex.Length;
    }
}