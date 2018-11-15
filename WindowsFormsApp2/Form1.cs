using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";
        }

        string inputnum;
        string leftnum;
        string rightnum;
        string ope;
        float left;
        float right;
        float result;
        
        //数字，文字をクリック
        private void Form1_Click(object sender, EventArgs e)
        {
            inputnum += ((Button)sender).Text;
            textBox1.Text = inputnum;
        }
        //演算子をクリック
        private void textBox1_Click(object sender, EventArgs e)
        {
            if (leftnum != null)//＝を押さずに演算を続ける場合
            {
                rightnum = textBox1.Text;//右辺にテキストを保存

                //文字列から数値へ変換
                left = Convert.ToSingle(leftnum);
                right = Convert.ToSingle(rightnum);

                switch (ope)//それぞれの演算子によって計算方法を変える
                {
                    case "+":
                        result = left + right;
                        break;

                    case "-":
                        result = left - right;
                        break;

                    case "×":
                        result = left * right;
                        break;

                    case "÷":
                        result = left / right;
                        break;
                }
                textBox1.Text = result.ToString();//数値から文字列へ
                leftnum = textBox1.Text;//演算結果のテキストをを左辺に保存
                inputnum = null;//入力値を初期化
            }
            else
            {
                leftnum = textBox1.Text;//左辺にテキストを保存
                inputnum = null;//入力文字列を初期化
                ope = ((Button)sender).Text;//演算子の保存
            }
        }
        //＝をクリック
        private void button17_Click(object sender, EventArgs e)
        {
            rightnum = textBox1.Text;//右辺にテキストを保存

            //文字列から数値へ変換
            left = Convert.ToSingle(leftnum);
            right = Convert.ToSingle(rightnum);

            switch (ope)//それぞれの演算子によって計算方法を変える
            {
                case "+":
                    result = left + right;
                    break;

                case "-":
                    result = left - right;
                    break;

                case "×":
                    result = left * right;
                    break;

                case "÷":
                    if(right == 0)//0で割るとき，メッセージボックスを表示して，全て初期化して帰る
                    {
                        MessageBox.Show("0で除算出来ません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = "0";
                        inputnum = null;
                        rightnum = null;
                        leftnum = null;
                        ope = null;
                        right = 0;
                        left = 0;
                        result = 0;
                        return;
                    }
                    else
                    {
                        result = left / right;
                    }
                    break;
            }
            textBox1.Text = result.ToString();//数値から文字列へ
            leftnum = textBox1.Text;//演算結果のテキストをを左辺に保存
            inputnum = null;//入力値を初期化
        }
        //クリアエントリー(CE) 直前に入力していた数字を取り消す
        private void button18_Click(object sender, EventArgs e)
        {
            if(rightnum == null)//初めての演算結果が出る前にCEがクリックされた時
            {
                inputnum = null;
                textBox1.Text = "0";
            }
            else//演算結果が一回でも出た後にCEがクリックされた時
            {
                inputnum = null;
                textBox1.Text = "0";
                leftnum = rightnum;//右辺を左辺に代入
            } 
        }
        //全ての数値を初期化
        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            inputnum = null;
            rightnum = null;
            leftnum = null;
            ope = null;
            right = 0;
            left = 0;
            result = 0;            
        }
    }
}