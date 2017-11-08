﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace huffmanProject
{
    
        class HuffmanNode
    {
        public string symbol;   
        public int frequency;        
        public string code;           
        public HuffmanNode parentNode;
        public HuffmanNode leftTree;  
        public HuffmanNode rightTree;  
        public bool isLeaf;           


        public HuffmanNode(string value)   
        {
            symbol = value;     
            frequency = 1;

            rightTree =null; 
            leftTree = null;
            parentNode = null;       
            code = "";        
            isLeaf = true;    
        }


        public HuffmanNode(HuffmanNode node1, HuffmanNode node2)
        {
           
            code = "";
            isLeaf = false;
            parentNode = null;
            // JIS KI FREQUENCY ZIADA HIA WOH RIGHT P LAGEGA HMAESHA
                 if (node1.frequency >= node2.frequency)
            {
                rightTree = node1;
                leftTree = node2;
                rightTree.parentNode = leftTree.parentNode = this;
                symbol = node1.symbol + node2.symbol;
                frequency = node1.frequency + node2.frequency;
            }
            else if (node1.frequency < node2.frequency)
            {
                rightTree = node2;
                leftTree = node1;
                leftTree.parentNode = rightTree.parentNode = this;     
                symbol = node2.symbol + node1.symbol;
                frequency = node2.frequency + node1.frequency;
            }
        }




        public void frequencyIncrease()          
        {
            frequency++;
        }
 
    }
}
