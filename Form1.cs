﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_calculator
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();
        
        #region Events
        void btn1_Click(object sender, EventArgs e) => UpdateTextField("1");
        void btn2_Click(object sender, EventArgs e) => UpdateTextField("2");
        void btn3_Click(object sender, EventArgs e) => UpdateTextField("3");
        void btn4_Click(object sender, EventArgs e) => UpdateTextField("4");
        void btn5_Click(object sender, EventArgs e) => UpdateTextField("5");
        void btn6_Click(object sender, EventArgs e) => UpdateTextField("6");
        void btn7_Click(object sender, EventArgs e) => UpdateTextField("7");
        void btn8_Click(object sender, EventArgs e) => UpdateTextField("8");
        void btn9_Click(object sender, EventArgs e) => UpdateTextField("9");
        void btn0_Click(object sender, EventArgs e) => UpdateTextField("0");
        void btnDecimal_Click(object sender, EventArgs e) => UpdateTextField(".");
        void btnPlus_Click(object sender, EventArgs e) => Operate('+');
        void btnMinus_Click(object sender, EventArgs e) => Operate('-');
        void btnMultiply_Click(object sender, EventArgs e) => Operate('*');
        void btnDivide_Click(object sender, EventArgs e) => Operate('/');
        void btnReset_Click(object sender, EventArgs e) => Reset();               
        #endregion

        
        #region Event Handlers
        void UpdateTextField(string value) => txtfield.Text = $"{txtfield.Text}{value}";
        void Reset() => txtfield.Text = string.Empty;
        void ClearLastChar(string s) => txtfield.Text = s.Length > 0 ? s.Substring(0, s.Length - 1) : txtfield.Text;
        #endregion


        #region Calculator Logic
        void Operate(char symbol) => txtfield.Text = IsMultiValue() ? Calculate(symbol) : (IsDoubleSymbol(txtfield.Text,txtfield.Text.Length-1) ? txtfield.Text : $"{txtfield.Text}{symbol}");            
        
        bool IsMultiValue() { return (IsSymbolExist() && txtfield.Text.Split(GetFirstSymbol()).Length > 1) ? true : false; }

        bool IsDoubleSymbol(string s, int i) { return (s.Length < 1) ? true : (s[i] == '+' || s[i] == '-' || s[i] == '*' || s[i] == '/'); }
        
        bool IsSymbolExist() { return (txtfield.Text.Where<char>(s => s == '+' || s == '-' || s == '*' || s == '/').FirstOrDefault().ToString().Length > 0) ? true : false; }

        char GetFirstSymbol() { return txtfield.Text.Where<char>(s => s == '+' || s == '-' || s == '*' || s == '/').FirstOrDefault(); }
        
        char GetLastSymbol() { return txtfield.Text.Where<char>(s => s == '+' || s == '-' || s == '*' || s == '/').Last(); }

        string Calculate(char symbol)
        {

            Console.WriteLine(GetLastSymbol() + "Last Symbol");
            Console.WriteLine(GetFirstSymbol() + "First Symbol");

            if (IsDoubleSymbol(txtfield.Text, txtfield.Text.Length - 1))
                return txtfield.Text;

            var v = txtfield.Text.Split(GetLastSymbol());

            switch (symbol)
            {
                case '+': return (float.Parse(v[1]) + float.Parse(v[0])).ToString("F1") + symbol;
                case '-': return (float.Parse(v[1]) - float.Parse(v[0])).ToString("F1") + symbol;
                case '*': return (float.Parse(v[1]) * float.Parse(v[0])).ToString("F1") + symbol;
                case '/': return (float.Parse(v[1]) / float.Parse(v[0])).ToString("F1") + symbol;
            }
            return symbol.ToString();
        }
        #endregion
    }
}
