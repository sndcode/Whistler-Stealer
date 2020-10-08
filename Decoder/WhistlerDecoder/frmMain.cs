using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WhistlerDecoder
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public static string output;
        public class ROT13
        {
            public string Name { get { return "ROT13"; } }

            public static string Decode(string input)
            {
                return Run(input);
            }

            public static string Encode(string input)
            {
                return Run(input);
            }

            private static string Run(string value)
            {
                char[] array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                {
                    int number = (int)array[i];

                    if (number >= 'a' && number <= 'z')
                    {
                        if (number > 'm')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    else if (number >= 'A' && number <= 'Z')
                    {
                        if (number > 'M')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    array[i] = (char)number;
                }
                return new string(array);
            }
        }
        public class Lambda
        {
            static Func<string, int, string>
            x = (f, n) => new string(f.Select<char, char>(c => (char)(c ^ n)).ToArray());

            public static string Encode(string input, int salt)
            {
                return x(input, salt);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string input = richTextBox1.Text;
            string decoder01 = Lambda.Encode(input, 1432);
            string decoder02 = ROT13.Decode(decoder01);
            richTextBox1.Text = decoder02;
            output = decoder02;
            MessageBox.Show("Decoded!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            rnd.Next(999);
            string rndm = rnd.Next(999).ToString();
            string savetopath = Application.StartupPath + "\\decodedlog" + rndm + ".txt";
            System.IO.File.WriteAllText(savetopath, output);
            MessageBox.Show("Saved to : " + savetopath + " !");
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Clipboard.GetText();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string fullfilepath = ofd.FileName;
            string contents = System.IO.File.ReadAllText(fullfilepath);
            richTextBox1.Text = contents;
        }
    }
}
