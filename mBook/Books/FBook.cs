using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Collections;
using mBook.Books;
using EyeXFramework;
using Tobii.Interaction;
using System.Speech.Synthesis;

namespace mBook
{
    public partial class FBook : Form
    {
        public Hashtable m_htPointGaze;
        public int[] m_windowsLocationX;
        public int[] m_windowsLocationY;
        public string[] m_sWords;
        public CBook m_oBook;
        public CPage m_oPage;
        public int m_iActualPage;
        SpeechSynthesizer speaker;

        public FBook(CBook oBook)
        {
            InitializeComponent();

            speaker = new SpeechSynthesizer();

            Program.EyeXHost.Connect(behaviorMap1);
            //behaviorMap1.Add(btNext, new EyeXFramework.GazeAwareBehavior(OnGaze));
            behaviorMap1.Add(btNext, new EyeXFramework.GazeAwareBehavior(OnGazeClick) { DelayMilliseconds = 2000 });

            behaviorMap1.Add(btBack, new EyeXFramework.GazeAwareBehavior(OnGaze));
            behaviorMap1.Add(btBack, new EyeXFramework.ActivatableBehavior(OnButtonActivated));
            behaviorMap1.Add(richTextBox1, new EyeXFramework.GazeAwareBehavior(OnGazeRichTextBox));

            behaviorMap1.Add(button1, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button2, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button3, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button4, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button5, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button6, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button7, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button8, new EyeXFramework.GazeAwareBehavior(OnGazeWord));
            behaviorMap1.Add(button9, new EyeXFramework.GazeAwareBehavior(OnGazeWord));

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            m_oBook = oBook;
            m_iActualPage = 1;
            m_oPage = m_oBook.GetPage(m_iActualPage);
            GetPage();
                        
            m_sWords = new string[] {"Halloween", "brincar", "pizza", "floresta", "porta", "barulho", "monstro", "televisão"};
            btNext.Select();
           
        }

        private void ConnectSound()
        {
            SoundPlayer player = new SoundPlayer();

            player.SoundLocation = "imperial_march.wav";
            player.Play();
            
        }

        private void GetPage()
        {
            richTextBox1.ResetText();
            m_oPage = m_oBook.GetPage(m_iActualPage);

            for (int i=1; i<m_oPage.Lines.Count+1; i++)
            {
                richTextBox1.AppendText("\n\n");
                richTextBox1.AppendText(m_oPage.GetLine(i).Text);
                richTextBox1.AppendText("\n");
            }

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

            btNext.Visible = m_iActualPage == m_oBook.Pages.Count ? false : true;
            btBack.Visible = m_iActualPage == 1 ? false : true;

            label1.Text = m_oBook.Name;
            label2.Text = m_oPage.PageId.ToString() + " de " + m_oBook.Pages.Count.ToString();

            SetEffectWord();
        }

        private void SetEffectWord()
        {
            int index = 0;
            Point textBoxLocation;
            Point windowsLocation;

            if (m_sWords == null)
                return;

            for (int i = 0; i < m_sWords.Length; i++)
            {
                index = richTextBox1.Find(m_sWords[i]);
                textBoxLocation = richTextBox1.GetPositionFromCharIndex(index);
                windowsLocation = new Point(richTextBox1.Location.X + textBoxLocation.X, richTextBox1.Location.Y + textBoxLocation.Y);

                foreach (Control control in this.Controls)
                {
                    if (control.Name == "button" + (i + 1).ToString())
                    {
                        control.Location = new System.Drawing.Point(windowsLocation.X, windowsLocation.Y);
                        control.Text = m_sWords[i].ToString();
                        control.Visible = true;

                    }
                }
            }
        }


        #region Events

        private void OnGaze(object sender, GazeAwareEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.ForeColor = (e.HasGaze) ? Color.Green : Color.Gray;
                button.Select();
            }
        }

        private void OnGazeClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
                btNext_Click(sender, e);
        }

        private void OnGazeWord(object sender, GazeAwareEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if(e.HasGaze)
                    {
                    int index = richTextBox1.Find(button.Text);
                    if (index != -1)
                    {
                        richTextBox1.SelectionStart = index;
                        richTextBox1.SelectionLength = button.Text.Length;
                        richTextBox1.SelectionColor = Color.Red;
                        if(speaker.State!=SynthesizerState.Speaking)
                            speaker.SpeakAsync(button.Text);
                    }
                }
                else {}
            }
        }

        private void OnGazeRichTextBox(object sender, GazeAwareEventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            if (richTextBox != null)
            {
                richTextBox.ForeColor = (e.HasGaze) ? Color.Blue : Color.Gray;
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            m_iActualPage++;
            GetPage();

            foreach (Control ctr in this.Controls)
            {
                if (ctr.Name.Contains("button"))
                    ctr.Visible = true;
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            m_iActualPage--;
            GetPage();

            foreach (Control ctr in this.Controls)
            {
                if (ctr.Name.Contains("button"))
                    ctr.Visible = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            Console.WriteLine("OnKeyUp: " + keyEventArgs.KeyCode);

            if (keyEventArgs.KeyCode == Keys.ShiftKey)
            {
                Console.WriteLine("TriggerActivation");
                Program.EyeXHost.TriggerActivation();
            }
            keyEventArgs.Handled = false;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            // See PannableForms sample for an example how to disregard repeated KeyDown events.
            // We don't bother to do it in this example since most users do not press and hold down
            // the key for long, when clicking.
            Console.WriteLine("OnKeyDown: " + keyEventArgs.KeyCode);
            if (keyEventArgs.KeyCode == Keys.ShiftKey)
            {
                Console.WriteLine("TriggerActivationModeOn");
                Program.EyeXHost.TriggerActivationModeOn();
            }
            keyEventArgs.Handled = false;
        }

        /// <summary>
        /// Event handler invoked when a button is activated.
        /// </summary>
        /// <param name="sender">The control that received the gaze click.</param>
        private void OnButtonActivated(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Console.WriteLine("OnButtonActivated");
                button.PerformClick();
            }
        }

        private void FBook_Load(object sender, EventArgs e)
        {
            SetEffectWord();
        }

        #endregion

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
