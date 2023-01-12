using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgorithmsDataStructures2
{
    public abstract class BSTNode
    {
        public int NodeKey; // ключ узла
    }

    public class BSTNode<T>: BSTNode
    {
        //public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	
	
        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    // промежуточный результат поиска
    public class BSTFind<T>
    {
        // null если в дереве вообще нету узлов
        public BSTNode<T> Node;
	
        // true если узел найден
        public bool NodeHasKey;
	
        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;
	
        public BSTFind() { Node = null; }
    }

    public class BST<T>
    {
        BSTNode<T> Root; // корень дерева, или null
	
        public BST(BSTNode<T> node)
        {
            Root = node;
        }
	
        public BSTFind<T> FindNodeByKey(int key)
        {
            // если в дереве вообще нету узлов
            if (Root == null)
            {
                return new BSTFind<T>();
            }

            // ищем в дереве узел и сопутствующую информацию по ключу
            return FindByKey(Root, null, key);
        }
	
        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            var node = FindNodeByKey(key);
            if (node.NodeHasKey)
                return false; // если ключ уже есть

            // добавляем корень
            if (node.Node == null)
            {
                Root = new BSTNode<T>(key, val, null);
                return true;
            }
            
            var newNode = new BSTNode<T>(key, val, node.Node);
            if (node.ToLeft)
                node.Node.LeftChild = newNode;
            else
                node.Node.RightChild = newNode;

            return true;
        }
	
        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальный/минимальный ключ в поддереве
            return GetMinMax(FromNode, FindMax);
        }
	
        public bool DeleteNodeByKey(int key)
        {
            // удаляем узел по ключу
            var node = FindNodeByKey(key);
            if (!node.NodeHasKey)
                return false; // если узел не найден

            var parent = node.Node.Parent;
            // если удаляем корень
            if (parent == null)
            {
                Root = null;
                return true;
            }
            
            // флаг каким был у родителя - правым или левым
            bool deletedWasLeft = key < parent.NodeKey;
            var leftChild = node.Node.LeftChild;
            var rightChild = node.Node.RightChild;

            // находим узел-преемник, который встает вместо удаляемого
            var nodeToChange = GetNodeToChange(node.Node);
            
            if (deletedWasLeft)
                parent.LeftChild = nodeToChange;
            else
                parent.RightChild = nodeToChange;
            
            node.Node = nodeToChange;

            // если удаляем лист
            if (nodeToChange == null)
                return true;

            // Если мы находим лист, то его и надо поместить вместо удаляемого узла.
            // делаем узел-приемник потомком родителя удаляемого узла
            if (nodeToChange.LeftChild == null && nodeToChange.RightChild == null)
            {
                if (leftChild != null  && !nodeToChange.Equals(leftChild))
                    leftChild.Parent = nodeToChange;
                if (rightChild != null && !nodeToChange.Equals(rightChild))
                    rightChild.Parent = nodeToChange;
                
                nodeToChange.Parent.LeftChild = null;
                nodeToChange.Parent = parent;
              
                if (!nodeToChange.Equals(leftChild))
                    nodeToChange.LeftChild = leftChild;
                if (!nodeToChange.Equals(rightChild))
                    nodeToChange.RightChild = rightChild;

                return true;
            }

            // Если мы находим узел, у которого есть только правый потомок,
            // то преемником берём этот узел, а вместо него помещаем его правого потомка.
            // делаем узел-приемник потомком родителя удаляемого узла
            if (leftChild != null)
                leftChild.Parent = nodeToChange;
            
            if (!nodeToChange.Equals(leftChild))
                nodeToChange.LeftChild = leftChild;
            nodeToChange.Parent = parent;
            return true;
        }

        private BSTNode<T> GetNodeToChange(BSTNode<T> node)
        {
            // значит мы удаляем leaf
            if (node.RightChild == null && node.LeftChild == null)
                return null;

            // значит есть только левые потомки
            if (node.RightChild == null)
                return FinMinMax(node.LeftChild, true); 
            
            return FinMinMax(node.RightChild, false);
        }

        public int Count()
        {
            if (Root == null)
                return 0;
            
            return GetAllNodes(Root).Count;
        }

        public List<BSTNode> WideAllNodes()
        {
            if (Root == null)
                return new List<BSTNode>();
            
            return WideAllNodes(Root);
        }

        public List<BSTNode> DeepAllNodes(int o)
        {
            if (Root == null)
                return new List<BSTNode>();
            
            //левое поддерево, корень, правое поддерево
            if (o == 0)
                return InOrder(Root);

            //левое поддерево, правое поддерево, корень
            if (o == 1)
                return PostOrder(Root);

            //корень, левое поддерево, правое поддерево
            if (o == 2)
                return PreOrder(Root);

            throw new ArgumentException("Param should be 0, 1 or 2");
        }
        
        private List<BSTNode<T>> GetAllNodes(BSTNode<T> currentNode)
        {
            var list = new List<BSTNode<T>>();
            list.Add(currentNode);

            if (currentNode.LeftChild != null)
            {
                list.AddRange(GetAllNodes(currentNode.LeftChild));
            }

            if (currentNode.RightChild != null)
            {
                list.AddRange(GetAllNodes(currentNode.RightChild));
            }

            return list;
        }

        private BSTFind<T> FindByKey(BSTNode<T> current, BSTNode<T> parent, int key)
        {
            if (current == null)
            {
                // ключ не найден - пишем кому присваиваем
                if (parent.NodeKey > key)
                    return new BSTFind<T>()
                    {
                        Node = parent,
                        NodeHasKey = false,
                        ToLeft = true
                    };
                
                return new BSTFind<T>()
                {
                    Node = parent,
                    NodeHasKey = false,
                    ToLeft = false
                };
            }
            
            if (current.NodeKey.Equals(key))
                return new BSTFind<T>()
                {
                    Node = current,
                    NodeHasKey = true
                };

            if (current.NodeKey > key)
                return FindByKey(current.LeftChild, current, key);
            else
                return FindByKey(current.RightChild, current, key);
        }

        public BSTNode<T> GetMinMax(BSTNode<T> currentNode, bool findMax)
        {
            if (currentNode.RightChild == null && findMax)
                return currentNode;

            if (currentNode.LeftChild == null && !findMax)
                return currentNode;
            
            if (findMax)
                return GetMinMax(currentNode.RightChild, findMax);
            
            return GetMinMax(currentNode.LeftChild, findMax);
        }

        private List<BSTNode> InOrder(BSTNode<T> current)
        {
            var nodes = new List<BSTNode>();

            if (current.LeftChild != null) 
                nodes.AddRange(InOrder(current.LeftChild));

            nodes.Add(current);
            
            if (current.RightChild != null)
                nodes.AddRange(InOrder(current.RightChild));

            return nodes;
        }
        
        // текущий узел (корень) проверяем в последнюю очередь
        private List<BSTNode> PostOrder(BSTNode<T> current)
        {
            var nodes = new List<BSTNode>();
            
            if (current.LeftChild != null)
                nodes.AddRange(PostOrder(current.LeftChild));
            
            if (current.RightChild != null)
                nodes.AddRange(PostOrder(current.RightChild));
            
            nodes.Add(current);
            
            return nodes;
        }
        
        // текущий узел (корень) проверяем в первую очередь
        private List<BSTNode> PreOrder(BSTNode<T> current)
        {
            var nodes = new List<BSTNode>();

            nodes.Add(current);
            
            if (current.LeftChild != null)
                nodes.AddRange(PreOrder(current.LeftChild));
            
            if (current.RightChild != null)
                nodes.AddRange(PreOrder(current.RightChild));

            return nodes;
        }

        private List<BSTNode> WideAllNodes(BSTNode<T> root)
        {
            var queue = new Queue<BSTNode>();
            queue.Enqueue(root);
            var allNodes = new List<BSTNode>();

            while (queue.Count != 0)
            {
                BSTNode current = queue.Dequeue();
                if (!(current is BSTNode<T> currentT))
                    throw new Exception();
                
                allNodes.Add(currentT);

                if (currentT.LeftChild != null)
                    queue.Enqueue(currentT.LeftChild);
                if (currentT.RightChild != null)
                    queue.Enqueue(currentT.RightChild);
            }
            
            return allNodes;
        }
    }
}