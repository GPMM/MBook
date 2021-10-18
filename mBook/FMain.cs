using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using mBook.Books;
using EyeXFramework;
using System.IO;
using System.Diagnostics;
using ControlHuePhilips;
using System.IO.Ports;
using Tobii.Interaction;
using Tobii.EyeX.Framework;
using System.Drawing;

namespace mBook
{
    public partial class FMain : Form
    {
        int m_iBookId = 0;

        FInterfaceSerial o_fInterfaceSerial;
        SerialPort oSerialPort;

        public FMain()
        {
            InitializeComponent();

            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(circularButton1, new EyeXFramework.GazeAwareBehavior(OnGazeCircularButton) { DelayMilliseconds = 1000 });

            //var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))))+ CGenDef.BooksDir + "\\";
           
            string sImage = "";
            foreach (CBook oBook in CConfig.Instance.Books.Values)
            {
                foreach (Control c in GetControls(splitContainer2.Panel1).Where(x => x is PictureBox))
                {
                    if (c.Name == ("pictureBox" + oBook.Id.ToString()))
                    {
                        sImage = CGenDef.BooksDir + oBook.NameId.ToString() + "\\cover.png";
                        ((PictureBox)c).Image = new Bitmap(sImage);
                        ((PictureBox)c).Image.Tag = oBook.Name;
                        behaviorMap1.Add(c, new EyeXFramework.GazeAwareBehavior(OnGaze));
                    }
                }
            }
        }

        public IEnumerable<Control> GetControls(Control c)
        {
            return new[] { c }.Concat(c.Controls.OfType<Control>()
                                              .SelectMany(x => GetControls(x)));
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var ctr = sender as PictureBox;
            if (ctr != null)
            {
               
                ctr.Select();

                richTextBox1.Clear();
                richTextBox1.AppendText("\n");
                richTextBox1.Text = "Título: " + ctr.Image.Tag.ToString();
                CBook oBook = CConfig.Instance.GetBook(ctr.Image.Tag.ToString());
                richTextBox1.AppendText("\n");
                richTextBox1.AppendText("ISBN: " + oBook.ISBN);
                richTextBox1.AppendText("\n");
                richTextBox1.AppendText("Autor: " + oBook.Author);
                richTextBox1.AppendText("\n");
                richTextBox1.AppendText("Editora: " + oBook.Editora);

                richTextBox2.Clear();
                richTextBox1.AppendText("\n");
                richTextBox2.AppendText("Descrição: " + oBook.Description);

                m_iBookId = oBook.Id;
                circularButton1.Location = new System.Drawing.Point(ctr.Location.X + ctr.Width - 30, ctr.Location.Y + ctr.Height - 30);
                
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;

            /*CBook oBook = CConfig.Instance.GetBook(pb.Image.Tag.ToString());
            FBook fBook = new FBook(oBook);
            fBook.Show();*/

            CBook oBook = CConfig.Instance.GetBook(pb.Image.Tag.ToString());
            if (oBook.Pages.Count != 0)
            {
                FBook fBook = new FBook(oBook, null);
                fBook.Show();
            }
            else
            {
                FChapter fChapter = new FChapter(oBook, this);
                fChapter.Show();
            }
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            CBook oBook = CConfig.Instance.GetBook(m_iBookId);
            if (oBook.Pages.Count != 0)
            {
                FBook fBook = new FBook(oBook, null);
                fBook.Show();
            }
            else
            {
                FChapter fChapter = new FChapter(oBook, this);
                fChapter.Show();
            }
        }

        private void OnGaze(object sender, GazeAwareEventArgs e)
        {
            var ctr = sender as PictureBox;
            if (ctr != null)
            {
                if (e.HasGaze)
                {
                    ctr.Select();
                    CBook oBook = CConfig.Instance.GetBook(ctr.Image.Tag.ToString());

                    richTextBox1.Clear();
                    richTextBox1.AppendText("\n");
                    richTextBox1.Text = "Título: " + ctr.Image.Tag.ToString();
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("ISBN: " + oBook.ISBN);
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("Autor: " + oBook.Author);
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("Editora: " + oBook.Editora);

                    richTextBox2.Clear();
                    richTextBox1.AppendText("\n");
                    richTextBox2.AppendText("Descrição: " + oBook.Description);

                    m_iBookId = oBook.Id;
                    circularButton1.Location = new System.Drawing.Point(ctr.Location.X+ctr.Width-30, ctr.Location.Y+ctr.Height-30);
                }
            }
        }

        private void OnGazeCircularButton(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                /*CBook oBook = CConfig.Instance.GetBook(m_iBookId);
                FBook fBook = new FBook(oBook);
                fBook.Show();*/
                CBook oBook = CConfig.Instance.GetBook(m_iBookId);
                if (oBook.Pages.Count != 0)
                {
                    FBook fBook = new FBook(oBook, null);
                    fBook.Show();
                }
                else
                {
                    FChapter fChapter = new FChapter(oBook,this);
                    fChapter.Show();
                }
            }
        }

        private void questionárioDeUsabilidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://forms.gle/7J1QbS8pEaPWbDGh8");
            Process.Start(sInfo);
            
        }

        private void calibraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.EyeXHost.LaunchRecalibration();
        }

        private void configurarLâmpadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FConfigBridge oConfigBridge = new FConfigBridge();
            oConfigBridge.ShowDialog();

            if (oConfigBridge.IP != "")
            {
                string sIP = oConfigBridge.IP;

                HueLogic.FindBridgeIP(sIP);
                MessageBox.Show("Aperte o botão da Philips Bridge.");
                HueLogic.ConnectBridge("Philips hue");
                HueLogic.GetBridge();
            }
        }

        private void configurarPortaSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (o_fInterfaceSerial == null)
            {
                o_fInterfaceSerial = new FInterfaceSerial();
                o_fInterfaceSerial.ShowDialog();

                oSerialPort = new SerialPort();
                oSerialPort = o_fInterfaceSerial.InterfaceSerialPort;

                CConfig.Instance.InterfaceSerial = o_fInterfaceSerial;
            }
            else
            {
                o_fInterfaceSerial.ShowDialog();
                oSerialPort = o_fInterfaceSerial.InterfaceSerialPort;
            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            pictureBox1.Select();
            CBook oBook = CConfig.Instance.GetBook(pictureBox1.Image.Tag.ToString());

            richTextBox1.Clear();
            richTextBox1.AppendText("\n");
            richTextBox1.Text = "Título: " + pictureBox1.Image.Tag.ToString();
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("ISBN: " + oBook.ISBN);
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("Autor: " + oBook.Author);
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("Editora: " + oBook.Editora);

            richTextBox2.Clear();
            richTextBox1.AppendText("\n");
            richTextBox2.AppendText("Descrição: " + oBook.Description);

            m_iBookId = oBook.Id;
            circularButton1.Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Width - 30, pictureBox1.Location.Y + pictureBox1.Height - 30);
            //circularButton1.BackColor = System.Drawing.Color.FromArgb(64, circularButton1.BackColor);
        }

        private void FMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            HueLogic.PutBridge(1, true, 0, 0, 50);
            HueLogic.PutBridge(2, true, 0, 0, 50);

            if (oSerialPort != null)
            {
                CConfig.Instance.InterfaceSerial.InterfaceSerialPort.Write("OUT00");
                CConfig.Instance.InterfaceSerial.InterfaceSerialPort.Write("OUT10");
                oSerialPort.Close();
            }

            //this.Close();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://eic.cefet-rj.br/~gpmm/portfolio/leitura-multissensorial/");
            Process.Start(sInfo);
        }

        private void ajudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void circularButton1_MouseEnter(object sender, EventArgs e)
        {
            CBook oBook = CConfig.Instance.GetBook(m_iBookId);

            if (oBook.Pages.Count != 0)
            {
                FBook fBook = new FBook(oBook, null);
                fBook.Show();
            }
            else
            {
                FChapter fChapter = new FChapter(oBook,this);
                fChapter.Show();
            }
        }

        private void sobreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://eic.cefet-rj.br/~gpmm/portfolio/leitura-multissensorial/");
            Process.Start(sInfo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            HueLogic.PutBridge(1, true, 0, 0, 50);
            HueLogic.PutBridge(2, true, 0, 0, 50);

            if (oSerialPort != null)
            {
                CConfig.Instance.InterfaceSerial.InterfaceSerialPort.Write("OUT00");
                CConfig.Instance.InterfaceSerial.InterfaceSerialPort.Write("OUT10");
                oSerialPort.Close();
            }

            this.Close();
        }
    }
}
