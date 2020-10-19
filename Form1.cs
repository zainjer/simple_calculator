using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace simple_calculator
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();
        #region Events
        void NumericBtnClick(object sender, EventArgs e) => UpdateTextField(((Button)sender).Text);            
        void ArithematicBtnClick(object sender, EventArgs e) => Operate(((Button)sender).Text[0]);
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
            if (IsDoubleSymbol(txtfield.Text, txtfield.Text.Length - 1)) { return txtfield.Text; }
            var _ = Split(txtfield.Text);
            switch (GetFirstSymbol())
            {
                case '+': return (_.v0 + _.v1).ToString("F1") + symbol;
                case '-': return (_.v0 - _.v1).ToString("F1") + symbol;
                case '*': return (_.v0 * _.v1).ToString("F1") + symbol;
                case '/': return (_.v0 / _.v1).ToString("F1") + symbol;
                default: return txtfield.Text;
            }
        }
        (float v0, float v1) Split(string exp)
        {
            exp = (exp[0] == '-') ? exp.Substring(1) : exp;
            bool isNegative = (txtfield.Text.Length > exp.Length) ? true : false;
            var _ = exp.Split(GetLastSymbol());
            return (isNegative ? float.Parse(_[0]) * (-1) : float.Parse(_[0]), float.Parse(_[1]));
        }
        #endregion
    }
}