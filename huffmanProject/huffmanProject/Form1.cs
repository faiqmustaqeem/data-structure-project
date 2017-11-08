using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace huffmanProject
{
    public partial class Form1 : Form
    {

        List<HuffmanNode> nodeList;//=new List<HuffmanNode>();
        Tree tree;// = new Tree();
        public Form1()
        {
            InitializeComponent();
            //nodeList=new List<HuffmanNode>();
            
        }
        string text = "";
        string encoded = "";
        string path = "";
        int noOfZeroAdded = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            label1.Text = openFileDialog1.FileName;
            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            text = sr.ReadToEnd();
            FileInfo f = new FileInfo(openFileDialog1.FileName);

            label5.Text = label5.Text +" "+ f.Length+" bytes.";
              

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            label4.Text = folderBrowserDialog1.SelectedPath;
            path = folderBrowserDialog1.SelectedPath;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tree = new Tree(text);
            nodeList = tree.makeNodesAndSetFrequencies();
            tree.createTreeFromList(nodeList);
            tree.setCodeToTheTree("", nodeList[0]);
            tree.addCodesInDict(nodeList[0]);
            encoded=tree.encodedString();
           // MessageBox.Show(encoded);
            writeEncodedbitsatSelectedLocation(path);
            FileInfo f = new FileInfo(path + "\\encoded.txt");
            label6.Text += f.Length.ToString() + " bytes.";
           
            
        }
        private void writeEncodedbitsatSelectedLocation(string path)
        {
            StreamWriter sw = new StreamWriter(path + "\\encoded.txt");
            string compresd = "";

            
            string s = encoded;
           
            while(s.Length > 0)
            {
                string sub = s.Substring(0, 8);
                s = s.Substring(8);
                var number = Convert.ToInt32(sub, 2);
                compresd += ((char)number).ToString();
                

                if(s.Length < 8 && s.Length !=0)
                {
                    int l = s.Length;
                    noOfZeroAdded = 8-l;
                    for (int j = 0; j < noOfZeroAdded; j++)
                    {
                        s += "0";
                    }
                    
                    

                }
            }
            sw.Write(compresd);
            sw.Close();
            MessageBox.Show("File Copressed !!!");
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader sr=new StreamReader(path + "\\encoded.txt");
            string s = sr.ReadToEnd();
            string bits = "";
            for (int i = 0; i < s.Length; i++)
            {
                int number = (int)s[i];
                string binary = Convert.ToString(number, 2);
                if (binary.Length < 8)
                {
                    while (binary.Length != 8)
                    {
                        binary = binary.Insert(0, "0");
                    }
                }

                if(i==s.Length-1)
                {
                    binary = binary.Substring(0, 8 - noOfZeroAdded);
                }
              
                bits += binary;
                
            }
            Dictionary<string, string> dictionary = tree.returnDictionary();

            string message = "";
            string str = "";
            for (int i = 0; i < bits.Length; i++)
            {
                str+=bits[i];
                if (dictionary.ContainsValue(str))
                {
                    foreach (var item in dictionary)
                    {
                        if (item.Value == str)
                        {
                            message += item.Key;
                            break;
                        }
                    }
                    str = "";
                }
            }
            StreamWriter sw = new StreamWriter(path + "\\decoded.txt");
            sw.Write(message);
            sw.Close();
            MessageBox.Show("Decompressed !!!!");
        }
    }
}
