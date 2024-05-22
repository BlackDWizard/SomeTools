using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestForm
{
    public partial class GenerateIDForm : Form
    {
        public GenerateIDForm()
        {
            InitializeComponent();

            DataTable dtCity = new DataTable();

            dtCity.Columns.Add("Value", typeof(int));
            dtCity.Columns.Add("Name", typeof(string));

            dtCity.Rows.Add(1, "臺北市(A)");
            dtCity.Rows.Add(2, "臺中市(B)");
            dtCity.Rows.Add(3, "基隆市(C)");
            dtCity.Rows.Add(4, "臺南市(D)");
            dtCity.Rows.Add(5, "高雄市(E)");
            dtCity.Rows.Add(6, "臺北縣(F)");
            dtCity.Rows.Add(7, "宜蘭縣(G)");
            dtCity.Rows.Add(8, "桃園縣(H)");
            dtCity.Rows.Add(9, "嘉義市(I)");
            dtCity.Rows.Add(10, "新竹縣(J)");
            dtCity.Rows.Add(11, "苗栗縣(K)");
            dtCity.Rows.Add(12, "臺中縣(L)");
            dtCity.Rows.Add(13, "南投縣(M)");
            dtCity.Rows.Add(14, "彰化縣(N)");
            dtCity.Rows.Add(15, "新竹市(O)");
            dtCity.Rows.Add(16, "雲林縣(P)");
            dtCity.Rows.Add(17, "嘉義縣(Q)");
            dtCity.Rows.Add(18, "臺南縣(R)");
            dtCity.Rows.Add(19, "高雄縣(S)");
            dtCity.Rows.Add(20, "屏東縣(T)");
            dtCity.Rows.Add(21, "花蓮縣(U)");
            dtCity.Rows.Add(22, "臺東縣(V)");
            dtCity.Rows.Add(23, "金門縣(W)");
            dtCity.Rows.Add(24, "澎湖縣(X)");
            dtCity.Rows.Add(25, "陽明山(Y)");
            dtCity.Rows.Add(26, "連江縣(Z)");

            DataTable dtBatchOption = new DataTable();

            dtBatchOption.Columns.Add("Value", typeof(int));
            dtBatchOption.Columns.Add("Name", typeof(string));

            dtBatchOption.Rows.Add(1, "身分證號");
            dtBatchOption.Rows.Add(2, "居留證號");
            dtBatchOption.Rows.Add(3, "統編");

            comboBox2.DataSource = dtCity;
            comboBox2.ValueMember = "Value";
            comboBox2.DisplayMember = "Name";

            comboBox1.DataSource = dtBatchOption;
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Name";

            tbCheckResult.Enabled = false;

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(tbOutput.Text);
            MessageBox.Show("複製成功");
        }

        private void btnIDNO_Click(object sender, EventArgs e)
        {
            bool sex = false;
            if (rbM.Checked)
                sex = true;
            if (rbF.Checked)
                sex = false;

            Random r = new Random();
            int rand = r.Next(0, 10000000);

            tbOutput.Text = CreateID(sex, comboBox2.SelectedIndex, rand);
        }

        private void btnRPNO_Click(object sender, EventArgs e)
        {
            bool sex = false;
            if (rbM.Checked)
                sex = true;
            if (rbF.Checked)
                sex = false;

            Random r = new Random();
            int rand = r.Next(0, 10000000);

            tbOutput.Text = CreateRPNO(sex, comboBox2.SelectedIndex, rand);
        }

        private void btnCNO_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int rand = r.Next(0, 10000000);

            tbOutput.Text = CreateCNO(rand);
        }

        private void btnBatchGenerate_Click(object sender, EventArgs e)
        {
            tbOutput.MaxLength = 32767;
            tbOutput.Text = "";

            bool sex = false;
            if (rbM.Checked)
                sex = true;
            if (rbF.Checked)
                sex = false;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Random r = new Random();

                    for (int i = 0; i < 100; i++)
                    {
                        int rand = r.Next(0, 10000000);
                        tbOutput.Text += (i + 1).ToString() + " : " + CreateID(sex, comboBox2.SelectedIndex, rand) + "\r\n";
                    }
                    break;
                case 1:
                    r = new Random();

                    for (int i = 0; i < 100; i++)
                    {
                        int rand = r.Next(0, 10000000);
                        tbOutput.Text += (i + 1).ToString() + " : " + CreateRPNO(sex, comboBox2.SelectedIndex, rand) + "\r\n";
                    }
                    break;
                case 2:
                    r = new Random();

                    for (int i = 0; i < 100; i++)
                    {
                        int rand = r.Next(0, 10000000);
                        tbOutput.Text += (i + 1).ToString() + " : " + CreateCNO(rand) + "\r\n";
                    }
                    break;
            }

            tbOutput.MaxLength = 10;
        }

        private void btnCheckIDNO_Click(object sender, EventArgs e)
        {

            if (tbOutput.Text.Substring(1, 1) != "1" && tbOutput.Text.Substring(1, 1) != "2")
            {
                MessageBox.Show("第二碼須為1或2");
                tbCheckResult.Text = "身分證字號NO PASS";
                return;
            }
            else if (tbOutput.Text.Length != 10)
            {
                MessageBox.Show("長度必須為10");
                tbCheckResult.Text = "身分證字號NO PASS";
                return;
            }

            if (CheckID(tbOutput.Text))
                tbCheckResult.Text = "身分證字號PASS";
            else
                tbCheckResult.Text = "身分證字號NO PASS";
        }

        private void btnCheckRPNO_Click(object sender, EventArgs e)
        {
            string firstLetter = tbOutput.Text.Substring(0, 1);
            string num = tbOutput.Text.Substring(1, tbOutput.Text.Length - 1);

            if (num.Substring(0, 1) != "8" && num.Substring(0, 1) != "9")
            {
                MessageBox.Show("第二碼須為8或9");
                tbCheckResult.Text = "居留證號NO PASS";
                return;
            }
            else if (tbOutput.Text.Length != 10)
            {
                MessageBox.Show("長度必須為10");
                tbCheckResult.Text = "居留證號NO PASS";
                return;
            }

            if (CheckNewResidentID(firstLetter, num))
                tbCheckResult.Text = "居留證號PASS";
            else
                tbCheckResult.Text = "居留證號NO PASS";
        }

        private void btnCheckCNO_Click(object sender, EventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            if (regex.IsMatch(tbOutput.Text))
            {
                MessageBox.Show("只能為純數字");
                tbCheckResult.Text = "統編NO PASS";
                return;
            }
            if (tbOutput.Text.Length != 8)
            {
                MessageBox.Show("統編長度須為8");
                tbCheckResult.Text = "統編NO PASS";
                return;
            }

            if (CheckUID(tbOutput.Text))
                tbCheckResult.Text = "統編PASS";
            else
                tbCheckResult.Text = "統編NO PASS";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string CreateID(bool sex, int city, int rand)
        {
            string[] county_E = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                      "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //對應數字 (索引值)
            int[] county_i = { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35,
                       23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
            string id = county_E[city];
            int c_i = county_i[city];
            string s = "2";
            if (sex) s = "1";
            //計算
            int check = (c_i / 10) + 9 * (c_i - (c_i / 10) * 10) + Convert.ToInt32(s) * 8;
            for (int i = 7; i >= 1; i--)
            {
                check += ((rand / (int)Math.Pow(10, i - 1)) % 10) % 10 * i;
            }
            check = (10 - (check % 10)) % 10;
            //計算審核碼
            id += s + rand.ToString().PadLeft(7, '0') + check.ToString();
            return id;
        }
        private string CreateRPNO(bool sex, int city, int rand)
        {
            string[] county_E = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                      "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //對應數字 (索引值)
            int[] county_i = { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35,
                       23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
            string id = county_E[city];
            int c_i = county_i[city];
            string s = "9";
            if (sex) s = "8";
            //計算
            int check = (c_i / 10) + 9 * (c_i % 10) + Convert.ToInt32(s) * 8;
            for (int i = 7; i >= 1; i--)
            {
                check += ((rand / (int)Math.Pow(10, i - 1)) % 10) % 10 * i;
            }
            check = (10 - (check % 10)) % 10;
            //計算審核碼
            id += s + rand.ToString().PadLeft(7, '0') + check.ToString();
            return id;
        }
        private string CreateCNO(int rand)
        {
            string result = "";
            int[] sevenDigits = rand.ToString().PadLeft(7, '0')
                                  .ToCharArray()
                                  .Select(c => Convert.ToInt32(c.ToString()))
                                  .ToArray();

            int[] weight = new int[] { 1, 2, 1, 2, 1, 2, 4 };
            int sum = 0;
            if (sevenDigits[6] != 7)
            {
                for (int i = 0; i < weight.Length; i++)
                {
                    int buffer = sevenDigits[i] * weight[i];
                    sum += (buffer / 10 + buffer % 10);
                }

                for (int i = 0; i < 10; i++)
                {
                    if ((sum + i) % 5 == 0)
                    {
                        result = string.Join("", new List<int>(sevenDigits).ConvertAll(digit => digit.ToString()).ToArray()) + i.ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < weight.Length - 1; i++)
                {
                    int buffer = sevenDigits[i] * weight[i];
                    sum += (buffer / 10 + buffer % 10);
                }

                sum++;

                for (int i = 0; i < 10; i++)
                {
                    if ((sum + i) % 5 == 0 || (sum - 1 + i) % 5 == 0)
                    {
                        result += "\r\n(第七碼為7) : " + string.Join("", new List<int>(sevenDigits).ConvertAll(digit => digit.ToString()).ToArray()) + i.ToString();
                    }
                }
            }

            return result;

        }
        private bool CheckID(string id)
        {
            // 使用「正規表達式」檢驗格式 [A~Z] {1}個數字 [0~9] {9}個數字
            var regex = new Regex("^[A-Z]{1}[0-9]{9}$");
            if (!regex.IsMatch(id))
            {
                //Regular Expression 驗證失敗，回傳 ID 錯誤
                return false;
            }

            //除了檢查碼外每個數字的存放空間 
            int[] seed = new int[10];

            //建立字母陣列(A~Z)
            //A=10 B=11 C=12 D=13 E=14 F=15 G=16 H=17 J=18 K=19 L=20 M=21 N=22
            //P=23 Q=24 R=25 S=26 T=27 U=28 V=29 X=30 Y=31 W=32  Z=33 I=34 O=35            
            string[] charMapping = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
            string target = id.Substring(0, 1); //取第一個英文數字
            for (int index = 0; index < charMapping.Length; index++)
            {
                if (charMapping[index] == target)
                {
                    index += 10;
                    //10進制的高位元放入存放空間   (權重*1)
                    seed[0] = index / 10;

                    //10進制的低位元*9後放入存放空間 (權重*9)
                    seed[1] = (index % 10) * 9;

                    break;
                }
            }
            for (int index = 2; index < 10; index++) //(權重*8~1)
            {   //將剩餘數字乘上權數後放入存放空間                
                seed[index] = Convert.ToInt32(id.Substring(index - 1, 1)) * (10 - index);
            }
            //檢查是否符合檢查規則，10減存放空間所有數字和除以10的餘數的個位數字是否等於檢查碼            
            //(10 - ((seed[0] + .... + seed[9]) % 10)) % 10 == 身分證字號的最後一碼   
            if ((10 - (seed.Sum() % 10)) % 10 != Convert.ToInt32(id.Substring(9, 1)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 新式檢核
        /// </summary>
        /// <param name="firstLetter">第1碼英文字母(區域碼)</param>
        /// <param name="num">第2碼(性別碼) + 第3~9流水號 + 第10碼檢查碼</param>
        /// <returns></returns>
        private bool CheckNewResidentID(string firstLetter, string num)
        {
            ///建立字母對應表(A~Z)
            ///A=10 B=11 C=12 D=13 E=14 F=15 G=16 H=17 J=18 K=19 L=20 M=21 N=22
            ///P=23 Q=24 R=25 S=26 T=27 U=28 V=29 X=30 Y=31 W=32  Z=33 I=34 O=35 
            string alphabet = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            string transferIdNo = $"{(alphabet.IndexOf(firstLetter) + 10)}" +
                                  $"{num}";
            int[] idNoArray = transferIdNo.ToCharArray()
                                          .Select(c => Convert.ToInt32(c.ToString()))
                                          .ToArray();

            int sum = idNoArray[0];
            int[] weight = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 1 };
            for (int i = 0; i < weight.Length; i++)
            {
                sum += (weight[i] * idNoArray[i + 1]) % 10;
            }
            return (sum % 10 == 0);
        }
        public bool CheckUID(string idNo)
        {
            if (idNo == null)
            {
                return false;
            }
            Regex regex = new Regex(@"^\d{8}$");
            Match match = regex.Match(idNo);
            if (!match.Success)
            {
                return false;
            }
            int[] idNoArray = idNo.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
            int[] weight = new int[] { 1, 2, 1, 2, 1, 2, 4, 1 };

            int subSum;     //小和
            int sum = 0;    //總和
            int sumFor7 = 1;
            for (int i = 0; i < idNoArray.Length; i++)
            {
                subSum = idNoArray[i] * weight[i];
                sum += (subSum / 10)   //商數
                     + (subSum % 10);  //餘數                
            }
            if (idNoArray[6] == 7)
            {
                //若第7碼=7，則會出現兩種數值都算對，因此要特別處理。
                sumFor7 = sum + 1;
            }
            return (sum % 5 == 0) || (sumFor7 % 5 == 0);
        }
    }
}
//身分證開頭英文
/*              

 (1)英文代號以下表轉換成數字 
　　　A=10 台北市 city索引值(0)　　J=18 新竹縣　city索引值(9)　　 S=26 高雄縣 city索引值
　　　B=11 台中市 city索引值(1)　　K=19 苗栗縣　city索引值(10)　　T=27 屏東縣 city索引值(16) 
　　　C=12 基隆市 city索引值(2)　　L=20 台中縣　city索引值     　 U=28 花蓮縣 city索引值(17) 
　　　D=13 台南市 city索引值(3)　　M=21 南投縣　city索引值(11)　　V=29 台東縣 city索引值(18) 
　　　E=14 高雄市 city索引值(4)　　N=22 彰化縣　city索引值(12)　  W=32 金門縣 city索引值(19) 
　　　F=15 台北縣 city索引值(5)　　O=35 新竹市　city索引值(13)　　X=30 澎湖縣 city索引值(20) 
　　　G=16 宜蘭縣 city索引值(6)　　P=23 雲林縣　city索引值(14)　　Y=31 陽明山 city索引值 
　　　H=17 桃園縣 city索引值(7)　　Q=24 嘉義縣　city索引值(15)　  Z=33 連江縣 city索引值(21)
　　  I=34 嘉義市 city索引值(8)　　R=25 台南縣　city索引值
*/
