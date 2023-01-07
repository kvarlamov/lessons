using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
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
            return FindByKey(Root, key);
        }
	
        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            var node = FindNodeByKey(key);
            if (node.NodeHasKey)
                return false; // если ключ уже есть

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
           
            // флаг каким был у родителя - правым или левым
            bool deletedWasLeft = key < node.Node.Parent.NodeKey;
            var leftChild = node.Node.LeftChild;
            var rightChild = node.Node.RightChild;
            
            // находим узел-преемник, который встает вместо удаляемого
            var nodeToChange = GetNodeToChange(node.Node);
            
            if (deletedWasLeft)
                node.Node.Parent.LeftChild = nodeToChange;
            else
                node.Node.Parent.RightChild = nodeToChange;
            
            node.Node = nodeToChange;

            // Если мы находим лист, то его и надо поместить вместо удаляемого узла.
            // делаем узел-приемник потомком родителя удаляемого узла
            if (nodeToChange == null || (nodeToChange.LeftChild == null && nodeToChange.RightChild == null))
            {
                if (leftChild != null)
                    leftChild.Parent = nodeToChange;
                if (rightChild != null)
                    rightChild.Parent = nodeToChange;
                
                if (nodeToChange != null)
                {
                    nodeToChange.LeftChild = leftChild;
                    nodeToChange.RightChild = rightChild;
                }
                
                return true;
            }

            // Если мы находим узел, у которого есть только правый потомок,
            // то преемником берём этот узел, а вместо него помещаем его правого потомка.
            // делаем узел-приемник потомком родителя удаляемого узла

            if (leftChild != null)
                leftChild.Parent = nodeToChange;
            
            nodeToChange.LeftChild = leftChild;
            return true;
        }

        private BSTNode<T> GetNodeToChange(BSTNode<T> node)
        {
            // значит мы удаляем leaf
            if (node.RightChild == null)
                return null;

            return FinMinMax(node.RightChild, false);
        }

        public int Count()
        {
            return 0; // количество узлов в дереве
        }

        private BSTFind<T> FindByKey(BSTNode<T> currentNode, int key)
        {
            if (currentNode.NodeKey.Equals(key))
                return new BSTFind<T>()
                {
                    Node = currentNode,
                    NodeHasKey = true
                };
            
            if (currentNode.LeftChild == null && currentNode.RightChild == null)
            {
                // не найден - пишем кому присваиваем
                if (currentNode.NodeKey > key)
                    return new BSTFind<T>()
                    {
                        Node = currentNode,
                        NodeHasKey = false,
                        ToLeft = true
                    };
                
                return new BSTFind<T>()
                {
                    Node = currentNode,
                    NodeHasKey = false,
                    ToLeft = false
                };
            }
            
            if (currentNode.LeftChild == null || currentNode.RightChild == null)
            {
                if (currentNode.LeftChild == null && currentNode.RightChild.NodeKey.Equals(key))
                    return new BSTFind<T>()
                    {
                        Node = currentNode.RightChild,
                        NodeHasKey = true
                    };
                
                if (currentNode.RightChild == null && currentNode.LeftChild.NodeKey.Equals(key))
                    return new BSTFind<T>()
                    {
                        Node = currentNode.LeftChild,
                        NodeHasKey = true
                    };
                
                // не найден - пишем кому присваиваем
                if (currentNode.NodeKey > key)
                    return new BSTFind<T>()
                    {
                        Node = currentNode,
                        NodeHasKey = false,
                        ToLeft = true
                    };
                
                return new BSTFind<T>()
                {
                    Node = currentNode,
                    NodeHasKey = false,
                    ToLeft = false
                };
            }

            if (currentNode.NodeKey > key)
                return FindByKey(currentNode.LeftChild, key);
            
            return FindByKey(currentNode.RightChild, key);
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
    }
}