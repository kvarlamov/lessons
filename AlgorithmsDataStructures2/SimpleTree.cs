using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null
        public int Level;
	
        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }
	
    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }
	
        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            if (NewChild is null)
                throw new ArgumentException("argument can't be null");
            
            // код добавления нового дочернего узла существующему ParentNode
            if (Root is null || ParentNode is null)
            {
                // добавляем корень
                Root = NewChild;
                return;
            }

            if (ParentNode.Children == null)
                ParentNode.Children = new List<SimpleTreeNode<T>>();
            
            ParentNode.Children.Add(NewChild);
            NewChild.Parent = ParentNode;
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            // код удаления существующего узла NodeToDelete
            if (NodeToDelete.Parent == null)
            {
                Root = null;
            }
            else
            {
                NodeToDelete.Parent.Children.Remove(NodeToDelete);
            }

            // NodeToDelete.Children?.Clear();
            // NodeToDelete.Children = null;
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            // код выдачи всех узлов дерева в определённом порядке
            if (Root == null)
                return new List<SimpleTreeNode<T>>();
            
            if (Root.Children == null)
                return new List<SimpleTreeNode<T>>(){Root};
            
            return GetChildrens(Root);
        }
	
        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            // ваш код поиска узлов по значению
            return GetChildrensWithValue(Root, val);
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            // код перемещения узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
            if (OriginalNode == null)
                throw new ArgumentException("wrong param");
            
            DeleteNode(OriginalNode);
            AddChild(NewParent, OriginalNode);
        }
   
        public int Count()
        {
            // количество всех узлов в дереве
            return GetAllNodes().Count;
        }

        public int LeafCount()
        {
            // количество листьев в дереве
            return GetLeafs(Root).Count;
        }

        public void SetLevel()
        {
            var tree = GetAllNodes();
            Root.Level = 1;

            foreach (var node in tree)
            {
                if (node.Level == 1)
                    continue;

                node.Level = node.Parent.Level + 1;
            }
        }
        
        public List<T> EvenTrees()
        {
            var res = new List<T>();
            if (Root == null || Root.Children == null)
                return res;
            
            // 1. берем корневой узел
            // 2. идем по его потомкам (children)
            foreach (var node in Root.Children)
            {
                // если потомков нет - мы точно не получим чётное дерево 
                if (node.Children == null || node.Children.Count == 0)
                    continue;
                
                // если потомки есть - проверяем что их количество (всех в текущем поддереве) - чётное
                if (GetChildrens(node).Count % 2 == 0)
                    res.AddRange(new []{ Root.NodeValue, node.NodeValue });
            }

            return res;
        }

        

        private List<SimpleTreeNode<T>> GetChildrens(SimpleTreeNode<T> currentNode)
        {
            var list = new List<SimpleTreeNode<T>>();
            list.Add(currentNode);
            
            if (currentNode.Children == null || currentNode.Children.Count == 0)
            {
                return list;
            }

            foreach (var child in currentNode.Children)
            {
                list.AddRange(GetChildrens(child));
            }

            return list;
        }
        
        private List<SimpleTreeNode<T>> GetLeafs(SimpleTreeNode<T> currentNode)
        {
            var list = new List<SimpleTreeNode<T>>();

            if (currentNode.Children == null || currentNode.Children.Count == 0)
            {
                list.Add(currentNode);
                return list;
            }

            foreach (var child in currentNode.Children)
            {
                list.AddRange(GetLeafs(child));
            }

            return list;
        }
        
        private List<SimpleTreeNode<T>> GetChildrensWithValue(SimpleTreeNode<T> currentNode, T value)
        {
            var list = new List<SimpleTreeNode<T>>();
            if (currentNode.NodeValue.Equals(value))
                list.Add(currentNode);
            
            if (currentNode.Children == null || currentNode.Children.Count == 0)
            {
                return list;
            }

            foreach (var child in currentNode.Children)
            {
                list.AddRange(GetChildrensWithValue(child, value));
            }

            return list;
        }
    }
 
}