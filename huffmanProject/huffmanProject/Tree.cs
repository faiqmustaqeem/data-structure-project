using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace huffmanProject
{
    class Tree
    {
        
       public string str;
      public Tree(string str)
       {
           this.str = str;
       }
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public List<HuffmanNode> makeNodesAndSetFrequencies()
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>();  // Node List.

                for (int i = 0; i < str.Length; i++)
                {
                    string read = Convert.ToChar(str[i]).ToString();
                    if (contains(nodeList,read))
                        returnNode(nodeList,read).frequencyIncrease(); 
                    else
                        nodeList.Add(new HuffmanNode(read)); 
                }
                Sort(nodeList); 
                return nodeList;
            
        }

        public bool contains(List<HuffmanNode> nodeList, string symbol)
        {

            for (int i = 0; i <nodeList.Count; i++)
            {
                if (nodeList[i].symbol == symbol)
                {
                    return true;
                }
            }
            return false;
        }
        public HuffmanNode returnNode(List<HuffmanNode> nodeList, string symbol)
        {
            HuffmanNode node=null;
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].symbol == symbol)
                {
                    node = nodeList[i];
                    break;
                }
            }
            return node;
 
        }


       
        public void createTreeFromList(List<HuffmanNode> nodeList)
        {
            while (nodeList.Count > 1)  
            {
                HuffmanNode node1 = nodeList[0];    
                nodeList.RemoveAt(0);               
                HuffmanNode node2 = nodeList[0];    
                nodeList.RemoveAt(0);               
                nodeList.Add(new HuffmanNode(node1, node2));    
                Sort(nodeList); 
            }
        }


      
        public void setCodeToTheTree(string code, HuffmanNode Nodes)
        {
            if (Nodes == null)
                return;
            if (Nodes.leftTree == null && Nodes.rightTree == null)
            {
                Nodes.code = code;
                return;
            }

            setCodeToTheTree(code + "0", Nodes.leftTree);
            setCodeToTheTree(code + "1", Nodes.rightTree);
        }

        
        public void addCodesInDict(HuffmanNode nodeList)
        {
            if (nodeList == null)
                return;
            if (nodeList.leftTree == null && nodeList.rightTree == null)
            {
                dictionary.Add(nodeList.symbol.ToString(), nodeList.code.ToString());
                return;
            }
            addCodesInDict(nodeList.leftTree);
            addCodesInDict(nodeList.rightTree);
        }
        public string encodedString()
        {
            string encoded = "";
            for (int i = 0; i < str.Length; i++)
            {
                string j = str[i].ToString();
                encoded += dictionary[j];
            }
            return encoded;
        }
        public Dictionary<string, string> returnDictionary()
        {
            return dictionary;
        }

        public void Sort(List<HuffmanNode> nodeList)
        {
            for (int i = 1; i <= nodeList.Count - 1; i++)
            {
                for (int j = 0; j < nodeList.Count - 1; j++)
                {
                    if (nodeList[j + 1].frequency < nodeList[j].frequency)
                    {
                        HuffmanNode temp = nodeList[j];
                        nodeList[j] = nodeList[j + 1];
                        nodeList[j + 1] = temp;
                    }
                }
            }
        }


    }
}
