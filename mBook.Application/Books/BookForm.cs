using EyeXFramework;
using MBook.Domain;
using MBook.Domain.Entities;
using MBook.Domain.ValueObjects;
using MBook.Infrastructure.Context;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Tobii.Interaction;

namespace MBook.Books
{
    public partial class BookForm : Form
    {
        public Hashtable m_htPointGaze;
        public int[] m_windowsLocationX;
        public int[] m_windowsLocationY;
        public string[] m_sWords;
        public Book m_oBook;
        public Page m_oPage;
        public object m_IdChapter;
        public int m_iActualPage;
        TextWriter fileSave;
        StreamWriter portal1;
        Host host;

        private BookForm()
        {
            InitializeComponent();
        }

        public BookForm(Book oBook, object iIdChapter):this()
        {

            Program.EyeXHost.Connect(behaviorMap1);

            behaviorMap1.Add(btNext, new EyeXFramework.GazeAwareBehavior(OnGazeNextBack) { DelayMilliseconds = 300 });
            behaviorMap1.Add(btBack, new EyeXFramework.GazeAwareBehavior(OnGazeNextBack) { DelayMilliseconds = 300 });
            behaviorMap1.Add(btExit, new EyeXFramework.GazeAwareBehavior(OnGazeExit) { DelayMilliseconds = 2000 });

            // behaviorMap1.Add(btBack, new EyeXFramework.ActivatableBehavior(OnButtonActivated));

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            m_oBook = oBook;
            m_iActualPage = 1;
            m_IdChapter = iIdChapter;

            this.Text = GetTitle();
            lbTitle.Text = GetTitle();
            //GetPage();

            FillPage();
            btNext.Select();
        }

        private void GetPage()
        {
            if (m_IdChapter == null)
                m_oPage = m_oBook.GetPage(m_iActualPage);
            else
            {
                Chapter oChapter = (Chapter)m_oBook.Chapter[m_IdChapter];
                m_oPage = oChapter.GetPage(m_iActualPage);
            }
        }

        private int GetPagesCount()
        {
            if (m_IdChapter == null)
                return m_oBook.Pages.Count;
            else
            {
                Chapter oChapter = (Chapter)m_oBook.Chapter[m_IdChapter];
                return oChapter.Pages.Count;
            }
        }

        private string GetTitle()
        {
            if (m_IdChapter == null)
                return m_oBook.Name;
            else
            {
                Chapter oChapter = (Chapter)m_oBook.Chapter[m_IdChapter];
                return m_oBook.Name + " - " + oChapter.ChapterName;
            }
        }

        private void FillPage()
        {
            richTextBox1.Visible = false;
            richTextBox1.Clear();
            GetPage();
            if (m_oPage != null)
            {
                for (int i = 1; i < m_oPage.Lines.Count + 1; i++)
                {
                    richTextBox1.AppendText("\n");
                    richTextBox1.AppendText(m_oPage.GetLine(i).Text.ToUpper());
                    richTextBox1.AppendText("\n");
                }

                richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;

                LoadPicturePage();

                btNext.Visible = m_iActualPage == GetPagesCount() ? false : true;
                btBack.Visible = m_iActualPage == 1 ? false : true;

                lbPage.Text = m_iActualPage.ToString() + " de " + GetPagesCount().ToString();

                SetEffectPage();

                SetEffectWord();
            }
            richTextBox1.Visible = true;
        }

        private void LoadPicturePage()
        {
            string sImage = "";

            //splitContainer5.SplitterDistance = this.Width - 59;
            //splitContainer1.SplitterDistance = this.Width - splitContainer5.Width - 1;

            secundario_splitContainer.Panel1.BackgroundImage = null;
            GC.Collect();
            //if (pictureBox1.BackgroundImage != null)
            //{
            //    pictureBox1.BackgroundImage.Dispose();
            //    pictureBox1.BackgroundImage = null;
            //    GC.Collect();
            //}

            try
            {
                if (m_IdChapter == null)
                    m_oPage = m_oBook.GetPage(m_iActualPage);
                else
                {
                    Chapter oChapter = (Chapter)m_oBook.Chapter[m_IdChapter];
                    m_oPage = oChapter.GetPage(m_iActualPage);
                }
                sImage = GenDef.BooksDir + "\\" + m_oBook.NameId + "\\" + m_oPage.Background;
                // pictureBox1.BackgroundImage = new Bitmap(sImage);
                secundario_splitContainer.Panel1.BackgroundImage = new Bitmap(sImage);
            }
            catch
            {
                secundario_splitContainer.Panel1.BackgroundImage = new Bitmap(GenDef.BooksDir + m_oBook.NameId + "\\cover.png");
            }


            //if (pictureBox1.BackgroundImage.Width < this.Width - 119)
            //{
            //    splitContainer1.SplitterDistance = this.Width - pictureBox1.BackgroundImage.Width - 59;
            //    splitContainer5.SplitterDistance = pictureBox1.BackgroundImage.Width;
            //}
        }

        private void StartEffect(string EffectStream)
        {
            SoundPlayer player = new SoundPlayer();
            var mediaPlayer = new MediaPlayer.MediaPlayer();

            player.Stop();
            mediaPlayer.Stop();

            string[] codes = EffectStream.Split('|');
            string sEffectType = "";
            string sEffect = "";


            foreach (var code in codes)
            {
                if (code != "")
                {
                    sEffectType = code.Substring(0, 1);
                    sEffect =  Program.effectRepository.GetEffect(code);
                }

                try
                {
                    switch (sEffectType)
                    {
                        case "H":


                            if (sEffect.Contains("mp3"))
                            {
                                mediaPlayer.FileName = sEffect;
                                mediaPlayer.Play();
                            }
                            else
                            {
                                player.SoundLocation = sEffect;
                                player.Play();
                            }
                            break;

                        case "L":
                            int iBrightness1 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Brightness1=") + "Brightness1=".Length, 3));
                            int iBrightness2 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Brightness2=") + "Brightness2=".Length, 3));
                            int iSaturation1 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Saturation1=") + "Saturation1=".Length, 3));
                            int iSaturation2 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Saturation2=") + "Saturation2=".Length, 3));
                            int iHue1 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Hue1=") + "Hue1=".Length, 5));
                            int iHue2 = Convert.ToInt32(sEffect.Substring(sEffect.IndexOf("Hue2=") + "Hue2=".Length, 5));

                            Services.HueLogicService.PutBridge(1, true, iSaturation1, iBrightness1, iHue1);
                            Services.HueLogicService.PutBridge(2, true, iSaturation2, iBrightness2, iHue2);

                            break;

                        case "O":
                        case "A":
                            try
                            {
                                SerialContext.Instance.SerialPort.Write(sEffect);
                                //Config.Instance.InterfaceSerial.InterfaceSerialPort.Write(sEffect);
                            }
                            catch
                            {
                                SerialContext.Instance.SerialPort.Close();
                                //Config.Instance.InterfaceSerial.InterfaceSerialPort.Close();
                                return;
                            }

                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                }

            }
        }

        private void SetEffectPage()
        {
            StartEffect(m_oPage.Effects);
        }

        private void SetEffectWord()
        {
            int index = 0;
            Point textBoxLocation;
            Point windowsLocation;

            int j = 1;
            Hashtable ht = new Hashtable();

            foreach (Control ctr in this.Controls)
            {
                if (ctr.Name.Contains("button"))
                {
                    if (!ht.ContainsKey(ctr.Name))
                    {
                        ht.Add(j, ctr);
                        j++;
                    }
                }
            }

            foreach (Button bt in ht.Values)
            {
                bt.Visible = false;
                bt.Enabled = false;
                bt.Size = new System.Drawing.Size(0, 0);
                bt.Location = new System.Drawing.Point(0, 0);
                bt.SendToBack();
            }

            int i = 1;
            string s = "button";

            foreach (DictionaryEntry entry in m_oPage.Anchor)
            {
                index = richTextBox1.Find(entry.Key.ToString());
                textBoxLocation = richTextBox1.GetPositionFromCharIndex(index);
                windowsLocation = new Point(richTextBox1.Location.X + textBoxLocation.X, richTextBox1.Location.Y + textBoxLocation.Y);

                Button dynamicButton = CreateDynamicButton(s + i.ToString(), entry.Key.ToString(), entry.Value.ToString(), windowsLocation);

                behaviorMap1.Add(dynamicButton, new EyeXFramework.GazeAwareBehavior(OnGazeWord));

                i++;
            }
        }

        private Button CreateDynamicButton(string sName, string sText, string sTag, Point pLocation)
        {
            Button dynamicButton = new Button();

            dynamicButton.Name = sName;
            dynamicButton.Location = new Point(pLocation.X, pLocation.Y);
            dynamicButton.Text = sText;
            dynamicButton.Tag = sTag;

            dynamicButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            dynamicButton.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dynamicButton.Size = new System.Drawing.Size(195, 54);
            dynamicButton.UseVisualStyleBackColor = true;
            dynamicButton.Visible = false;
            dynamicButton.MouseEnter += new EventHandler(this.dynamicButton_MouseEnter);

            this.Controls.Add(dynamicButton);
            dynamicButton.BringToFront();

            return dynamicButton;
        }

        #region Events

        private void BookForm_Load(object sender, EventArgs e)
        {
            SetEffectPage();
            SetEffectWord();

            //string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string sFile = $"{Util.GetDirectoryPath("logGaze")}\\{DateTime.Now.ToFileTime()}.txt";
            // Criando arquivo para gravar log 
            FileStream fileStream = new FileStream(sFile, FileMode.Create);
            fileSave = Console.Out;
            portal1 = new StreamWriter(fileStream);
            Console.SetOut(portal1);

            host = new Host();
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();
            gazePointDataStream.GazePoint((x, y, ts) => Console.WriteLine("Timestamp: {0}\t X: {1} Y:{2}", ts, x, y));

            Console.WriteLine("Início da Leitura : " + m_oBook.Name);
            Console.WriteLine("Page : " + m_iActualPage + " Time : " + DateTime.Now);
        }

        private void OnGaze(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.ForeColor = Color.Green;
                button.Select();
            }
        }

        private void OnGazeNextBack(object sender, GazeAwareEventArgs e)
        {
            var button = sender as Button;
            button.Select();
            button.ForeColor = (e.HasGaze) ? Color.Green : Color.Gray;

            if (e.HasGaze)
            {
                if (button.Name == "btNext")
                {
                    btNext_Click(sender, e);
                    btNext.Select();
                }
                else
                {
                    btBack_Click(sender, e);
                    btBack.Select();
                }
            }
        }

        private void OnGazeExit(object sender, GazeAwareEventArgs e)
        {
            var button = sender as Button;
            button.Select();

            if (e.HasGaze)
                btExit_Click(sender, e);
        }

        private void OnGazeWord(object sender, GazeAwareEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (e.HasGaze)
                    StartEffect(button.Tag.ToString());
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
            Console.WriteLine("OnKeyDown: " + keyEventArgs.KeyCode);

            if (keyEventArgs.KeyCode == Keys.ShiftKey)
            {
                Console.WriteLine("TriggerActivationModeOn");
                Program.EyeXHost.TriggerActivationModeOn();
            }
            keyEventArgs.Handled = false;
        }
               

        private void btNext_Click(object sender, EventArgs e)
        {
            if (m_oPage.PageId != GetPagesCount())
            {
                m_iActualPage++;
                FillPage();
                Console.WriteLine("Page : " + m_iActualPage + " Time : " + DateTime.Now);
            }
            else
            {
                ProcessStartInfo sInfo = new ProcessStartInfo("https://forms.gle/7J1QbS8pEaPWbDGh8");
                Process.Start(sInfo);
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (m_oPage.PageId != 1)
            {
                m_iActualPage--;
                FillPage();
                Console.WriteLine("Page : " + m_iActualPage + " Time : " + DateTime.Now);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Page : " + m_iActualPage + " Time : " + DateTime.Now);
            Console.WriteLine("Fim da leitura");

            Console.SetOut(fileSave);
            portal1.Close();

            host.Dispose();

            this.Close();
        }

        private void dynamicButton_MouseEnter(object sender, EventArgs e)
        {

        }

        #endregion

        private void btNext_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Select();

            if (button.Name == "btNext")
            {
                btNext_Click(sender, e);
                btNext.Select();
            }
            else
            {
                btBack_Click(sender, e);
                btBack.Select();
            }
        }

        private void btBack_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Select();

            if (button.Name == "btNext")
            {
                btNext_Click(sender, e);
                btNext.Select();
            }
            else
            {
                btBack_Click(sender, e);
                btBack.Select();
            }
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lbTitle.Select();
        }
    }
}
