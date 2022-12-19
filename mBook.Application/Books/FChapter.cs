using EyeXFramework;
using MBook.Domain.Entities;
using MBook.Domain.ValueObjects;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MBook.Books
{
    public partial class FChapter : Form
    {
        Book m_oBook = null;
        Form m_fMain = null;

        public FChapter(Book oBook, Form Fmain)
        {
            InitializeComponent();

            m_oBook = oBook;
            m_fMain = Fmain;

            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(circularButton1, new GazeAwareBehavior(OnGazeCircularButton) { DelayMilliseconds = 1000 });

            this.Text = "MBook: " + oBook.Name;
            pictureBox1.Image = new Bitmap(GenDef.BooksDir + m_oBook.NameId + "\\cover.png");
            FillChapter(m_oBook.Chapter);
        }

        #region Private  Methods
        private void FillChapter(Hashtable htChapter)
        {
            Point windowsLocation;
            int y = 1;

            //ordenar por capitulo
            for (int i = 1; i < htChapter.Count + 1; i++)
            {
                Chapter oChapter = (Chapter)htChapter[i];

                windowsLocation = new Point(richTextBox1.Location.X + 20, y * 60);
                Button dynamicButton = CreateDynamicButton(oChapter.ChapterNameId, oChapter.ChapterName, oChapter.ChapterNumberId, windowsLocation);
                if (y == 1)
                {
                    circularButton1.Location = new Point(dynamicButton.Location.X + dynamicButton.Width + 30, dynamicButton.Location.Y);
                    dynamicButton.Select();
                }

                y++;
            }
            
        }
        private Button CreateDynamicButton(string sName, string sText, int sTag, Point pLocation)
        {
            Button dynamicButton = new Button();

            dynamicButton.Name = sName;
            dynamicButton.Location = new System.Drawing.Point(pLocation.X, pLocation.Y);
            dynamicButton.Text = sText;
            dynamicButton.TextAlign = ContentAlignment.MiddleLeft;
            dynamicButton.Tag = sTag;

            dynamicButton.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            dynamicButton.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dynamicButton.Size = new Size(550, 54);
            dynamicButton.UseVisualStyleBackColor = true;
            dynamicButton.Visible = true;
            dynamicButton.BackColor = Color.White;
            dynamicButton.FlatStyle = FlatStyle.Flat;
            dynamicButton.FlatAppearance.MouseOverBackColor = Color.White;
            dynamicButton.FlatAppearance.BorderColor = Color.White;

            behaviorMap1.Add(dynamicButton, new GazeAwareBehavior(OnGaze));

            dynamicButton.Click += new EventHandler(this.dynamicButton_click);
            dynamicButton.MouseEnter += new EventHandler(this.dynamicButton_mouseEnter);

            this.splitContainer1.Panel2.Controls.Add(dynamicButton);
            dynamicButton.BringToFront();

            return dynamicButton;

        }

        private void OnGazeCircularButton(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                BookForm bookForm = new BookForm(m_oBook, circularButton1.Tag);
                bookForm.Show();
                this.Close();
            }
        }

        #endregion

        private void OnGaze(object sender, GazeAwareEventArgs e)
        {
            var ctr = sender as Button;
            if (ctr != null)
            {
                if (e.HasGaze)
                {
                    ctr.Select();
                    circularButton1.Tag = ctr.Tag;
                    circularButton1.BringToFront();
                    circularButton1.Location = new System.Drawing.Point(ctr.Location.X + ctr.Width - 30, ctr.Location.Y + ctr.Height - 60);
                }
            }
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            //FBook fBook = new FBook(m_oBook, circularButton1.Tag);
            //fBook.Show();
            BookForm bookForm = new BookForm(m_oBook, circularButton1.Tag);
            bookForm.Show();
            this.Close();

        }

        private void dynamicButton_click(object sender, EventArgs e)
        {
            var ctr = sender as Button;
            if (ctr != null)
            {
                ctr.Select();
                circularButton1.Tag = ctr.Tag;
                circularButton1.BringToFront();
                circularButton1.Location = new System.Drawing.Point(ctr.Location.X + ctr.Width - 30, ctr.Location.Y + ctr.Height - 60);

            }
        }

        private void dynamicButton_mouseEnter(object sender, EventArgs e)
        {
            var ctr = sender as Button;
            if (ctr != null)
            {
                ctr.Select();
                circularButton1.Tag = ctr.Tag;
                circularButton1.BringToFront();
                circularButton1.Location = new Point(ctr.Location.X + ctr.Width - 30, ctr.Location.Y + ctr.Height - 60);
            }
        }

        private void circularButton1_MouseEnter(object sender, EventArgs e)
        {
            BookForm bookForm = new BookForm(m_oBook, circularButton1.Tag);
            bookForm.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_fMain.Select();
        }
    }
}
