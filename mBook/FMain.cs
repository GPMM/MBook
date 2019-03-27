using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mBook.Books;
using EyeXFramework.Forms;
using EyeXFramework;
using System.IO;
using Tobii.EyeX.Framework;

namespace mBook
{
    public partial class FMain : Form
    {
        int m_iBookId = 0;

        public FMain()
        {
            InitializeComponent();

            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(circularButton1, new EyeXFramework.GazeAwareBehavior(OnGazeCircularButton) { DelayMilliseconds = 2000 });

            string sImage;

            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))))+ CGenDef.ImageDir + "\\";

            foreach (CBook oBook in CConfig.Instance.Books.Values)
            {
                foreach (Control c in GetControls(splitContainer2.Panel1).Where(x => x is PictureBox))
                {
                    if (c.Name == ("pictureBox" + oBook.Id.ToString()))
                    {
                        //sImage = requiredPath + oBook.Id.ToString() + ".jpg";
                        //((PictureBox)c).Image = new Bitmap(sImage);
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
            
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;

            CBook oBook = CConfig.Instance.GetBook(pb.Image.Tag.ToString());
            FBook fBook = new FBook(oBook);
            fBook.Show();
        }

        private void calibraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.EyeXHost.LaunchRecalibration();
        }

        private void OnGaze(object sender, GazeAwareEventArgs e)
        {
            var ctr = sender as PictureBox;
            if (ctr != null)
            {
                if (e.HasGaze)
                {
                    ctr.Select();

                    richTextBox1.Clear();
                    richTextBox1.Text = "Título: " + ctr.Image.Tag.ToString();
                    CBook oBook = CConfig.Instance.GetBook(ctr.Image.Tag.ToString());
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("ISBN: " + oBook.ISBN);
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("Autor: " + oBook.Author);
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText("Editora: " + oBook.Editora);

                    richTextBox2.Clear();
                    richTextBox2.AppendText("Descrição: " + oBook.Description);

                    m_iBookId = oBook.Id;
                    circularButton1.Location = new System.Drawing.Point(ctr.Location.X+ctr.Width-30, ctr.Location.Y+ctr.Height-30);
                    //circularButton1.BackColor = System.Drawing.Color.FromArgb(64, circularButton1.BackColor);
                }
            }
        }
        private void OnGazeCircularButton(object sender, GazeAwareEventArgs e)
        {
            CBook oBook = CConfig.Instance.GetBook(m_iBookId);
            FBook fBook = new FBook(oBook);
            fBook.Show();
        }

        private void iniciarLogDeOlharToolStripMenuItem_Click(object sender, EventArgs e)
        {
                // Create a data stream: lightly filtered gaze point data.
                // Other choices of data streams include EyePositionDataStream and FixationDataStream.
                using (var lightlyFilteredGazeDataStream = Program.EyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered))
                {
                    // Write the data to the console.
                    lightlyFilteredGazeDataStream.Next += (s, f) => Console.WriteLine("Gaze point at ({0:0.0}, {1:0.0}) @{2:0}", f.X, f.Y, f.Timestamp);

                    // Let it run until a key is pressed.
                    Console.WriteLine("Listening for gaze data, press any key to exit...");
                    Console.In.Read();
                }
         }
    }
}
