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

        string  inputnum;
        string  leftnum;
        string  rightnum;
        string  ope;
        float   left;
        float   right;
        float   result;
        bool    minus = false;     /*フラグは，演算子，＝，C，CE,エラー表示がクリック，表示されたらfalseへ*/
        bool    dot = false;

/*****************************数字，文字をクリック********************************/
        private void Form1_Click(object sender, EventArgs e)
        {                                                /*-記号，”．”または”0”がクリックされた時の処理*/
            if ((((Button)sender).Text == "±") || (((Button)sender).Text == ".") || (((Button)sender).Text == "0"))
            {
                if (((Button)sender).Text == "±")       /*―記号がクリックされた時，文字の先頭に着き，連続で表示されない．*/
                {
                    if (minus)
                    {
                        return;
                    }
                    else
                    {
                        inputnum = "-" + inputnum;
                        textBox1.Text = inputnum;
                        minus = true;
                    }
                }

                if (((Button)sender).Text == ".")       /*"."記号がクリックされた時，文字の先頭で表示されず，連続で表示されない．*/
                {
                    if (inputnum == null)               /*何も数字がクリックされない状態で，”．”が押された時*/
                    {
                        dot = true;                     /*自動でドットの前に数字を表示*/
                        return;
                    }

                    if (dot)                            /*”．”がクリックされていたら，もう”．”を表示しない*/
                    {
                        return;
                    }
                    else
                    {
                        inputnum += ((Button)sender).Text;
                        textBox1.Text = inputnum;
                        dot = true;
                    }
                }

                if (((Button)sender).Text == "0")       /*"0"がクリックされた時，文字の先頭で表示されない.*/
                {
                    if( (inputnum == null) && (ope == null) )
                    {
                        return;
                    }
                    else
                    {
                        inputnum += ((Button)sender).Text;
                        textBox1.Text = inputnum;
                    }
                }      
            }
            else
            {
                if (dot == true && inputnum == null)    /*”．”が押された後に数字がきたら，先頭に0を表示する*/
                {
                    inputnum = "0" + "." + ((Button)sender).Text;
                    textBox1.Text = inputnum;
                    dot = false;
                }
                else                                    /*数字の表示*/
                {
                    inputnum += ((Button)sender).Text;
                    textBox1.Text = inputnum;
                }
            }
        }
/*************************演算子をクリック*****************************************/
        private void textBox1_Click(object sender, EventArgs e)
        {
            if (leftnum != null)                        /*＝を押さずに演算を続ける場合*/
            {
                if (inputnum == null)                   /*演算子だけが連続で押された時*/
                {
                    ope = ((Button)sender).Text;
                    return;
                }

                //rightnum = textBox1.Text;               /*右辺にテキストを保存*/
                 rightnum = inputnum;                      /*右辺に数字を保存*/

                /*文字列から数値へ変換,オーバーフロー処理*/
                /*変換成功で，outの変数へ，失敗でelseの処理*/
                if (float.TryParse(leftnum, out left) && (float.TryParse(rightnum, out right)))  
                {
                }
                else
                {
                    MessageBox.Show("表示できる桁数を超えています．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "0";
                    inputnum = null;
                    rightnum = null;
                    leftnum = null;
                    ope = null;
                    right = 0;
                    left = 0;
                    result = 0;
                    minus = false;
                    dot = false;
                    return;
                }
  
                switch (ope)                            /*それぞれの演算子によって計算方法を変える*/
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
                        if (right == 0)                      /*0で割るとき，メッセージボックスを表示して，全て初期化して帰る*/
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
                            minus = false;
                            dot = false;
                            return;
                        }
                        else
                        {
                            result = left / right;
                        }
                        break;
                }

                if (((-3.402823E+38 > result) || (result > 3.402823E+38)))
                {
                    MessageBox.Show("表示できる桁数を超えています．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "0";
                    inputnum = null;
                    rightnum = null;
                    leftnum = null;
                    ope = null;
                    right = 0;
                    left = 0;
                    result = 0;
                    minus = false;
                    dot = false;
                    return;
                }
                textBox1.Text = result.ToString();      /*数値から文字列へ*/
                leftnum = result.ToString();               /*演算結果のテキストをを左辺に保存*/
                inputnum = null;                        /*入力値を初期化*/
                ope = ((Button)sender).Text;            /*演算子の保存*/
                minus = false;
                dot = false;
            }
            else                                        /*最初の演算で=をクリックして答えを出す*/
            {
                if(inputnum == null)                    /*何も押されていない状態で演算子ボタンが連続で押された時*/
                {
                    return;
                }
                leftnum = textBox1.Text;                /*左辺にテキストを保存*/
                inputnum = null;                        /*入力文字列を初期化*/
                ope = ((Button)sender).Text;            /*演算子の保存*/
                minus = false;
                dot = false;
            }
        }
/*****************************＝をクリック*****************************/
        private void button17_Click(object sender, EventArgs e)
        {
            if( (inputnum == null) && (ope == null) && (textBox1.Text == "0"))                  /*=だけが最初に連続で押された時*/
            {
                return;
            }

            //rightnum = textBox1.Text;                   /*右辺にテキストを保存*/
            rightnum = inputnum;
            /***文字列から数値へ変換***/                /*変換成功で，outの変数へ，失敗でelseの処理*/
            if ( (float.TryParse(leftnum, out left)) && (float.TryParse(rightnum, out right)) )
            {                
            }
            else
            {
                MessageBox.Show("表示できる桁数を超えているか，数字が入力されていません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
                inputnum = null;
                rightnum = null;
                leftnum = null;
                ope = null;
                right = 0;
                left = 0;
                result = 0;
                minus = false;
                dot = false;
                return;
            }

            switch (ope)                                /*それぞれの演算子によって計算方法を変える*/
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
                    if(right == 0)                      /*0で割るとき，メッセージボックスを表示して，全て初期化して帰る*/
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
                        minus = false;
                        dot = false;
                        return;
                    }
                    else
                    {
                        result = left / right;
                    }
                    break;
            }
            /****オーバーフロー処理****/
            if (((-3.402823E+38 > result) || (result > 3.402823E+38)))
            {
                MessageBox.Show("表示できる桁数を超えています．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
                inputnum = null;
                rightnum = null;
                leftnum  = null;
                ope   =    null;
                right = 0;
                left = 0;
                result = 0;
                minus = false;
                dot = false;
                return;
            }
            else
            {
                textBox1.Text = result.ToString();             /*数値から文字列へ*/
            }
            leftnum = result.ToString();                       /*演算結果を左辺に保存*/
            //inputnum = null;                                   /*入力値を初期化*/
            minus = false;
            dot = false;
        }
/**************************クリアエントリー(CE) 直前に入力していた数字を取り消す*******************/
        private void button18_Click(object sender, EventArgs e)
        {
            if(rightnum == null)                                /*初めての演算結果が出る前にCEがクリックされた時*/
            {
                inputnum = null;
                textBox1.Text = "0";
                minus = false;
                dot = false;
            }
            else                                                /*演算結果が一回でも出た後にCEがクリックされた時*/
            {
                inputnum = null;
                textBox1.Text = "0";
                leftnum = rightnum;                             /*右辺を左辺に代入*/
                minus = false;
                dot = false;
            } 
        }
/*************************C:全ての数値を初期化******************************/
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
            minus = false;
            dot = false;
        }
    }
}