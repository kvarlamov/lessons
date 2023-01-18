using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey; // ключ узла
        public BSTNode Parent; // родитель или null для корня
        public BSTNode LeftChild; // левый потомок
        public BSTNode RightChild; // правый потомок	
        public int     Level; // глубина узла
	
        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class BalancedBST
    {
        public BSTNode Root; // корень дерева
	
        public BalancedBST() 
        { 
            Root = null;
        }
		
        public void GenerateTree(int[] a) 
        {  
            if (a== null || a.Length == 0)
                return;
            
            // создаём дерево с нуля из неотсортированного массива a
            Array.Sort(a);
            Root = Generate(Root, a, 0, a.Length - 1, 0);
        }

        public bool IsBalanced(BSTNode root_node)
        {
            if (root_node == null)
                return true;

            var leftDepth = GetDepth(root_node.LeftChild, root_node);
            var rightDepth = GetDepth(root_node.RightChild, root_node);
            
            // сбалансировано ли дерево с корнем root_node
            return Math.Abs(leftDepth - rightDepth) <= 1 
                   && IsBalanced(root_node.LeftChild) 
                   && IsBalanced(root_node.RightChild);
        }

        public List<BSTNode> GetNodes()
        {
            if (Root == null)
                return new List<BSTNode>();

            return InOrder(Root);
        }
        
        private List<BSTNode> InOrder(BSTNode current)
        {
            var nodes = new List<BSTNode>();

            if (current.LeftChild != null) 
                nodes.AddRange(InOrder(current.LeftChild));

            nodes.Add(current);
            
            if (current.RightChild != null)
                nodes.AddRange(InOrder(current.RightChild));

            return nodes;
        }

        private BSTNode Generate(BSTNode parent, int[] input, int left, int right, int depth)
        {
            if (left > right)
                return null;

            int centralIndex = GetCentralIndex(left, right);
            BSTNode newNode = new BSTNode(input[centralIndex], parent)
            {
                Level = depth
            };

            // Set Left
            var leftChild = Generate(newNode, input, left, centralIndex - 1, depth + 1);

            // Set Right
            var rightChild = Generate(newNode, input, centralIndex + 1, right, depth + 1);

            newNode.LeftChild = leftChild;
            newNode.RightChild = rightChild;

            return newNode;
        }

        private int GetCentralIndex(int left, int right) => (left + right) / 2;

        private int GetDepth(BSTNode node, BSTNode parent)
        {
            if (node == null)
                return parent.Level;

            int leftLevel = GetDepth(node.LeftChild, node);
            int rightLevel = GetDepth(node.RightChild, node);

            return Math.Max(leftLevel, rightLevel);
        }
    }
}