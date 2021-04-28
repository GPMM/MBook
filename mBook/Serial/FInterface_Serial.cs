using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; // Necessarário declarar para ter acesso a porta serial

namespace mBook
{
    public partial class FInterfaceSerial : Form
    {
        bool m_bFan2 = false;
        bool m_bSmell = false;

        // Váriaveis úteis 
        string rxString = " ";  // Recebe os dados da porta serial
        string serialHeader = " ";  // Cabeçalho inserido no protocolo de dados 
        string serialString = " ";  // Dados úteis inseridos no protocolo de dados
        int lengthSerialString = 0; // Comprimento da string a ser transmitda pela porta serial

        public SerialPort InterfaceSerialPort
        {
            get { return serialPort1; }
        }

        public int LengthSerialString
        {
            set { lengthSerialString = value; }
        }


        // Método principal
        public FInterfaceSerial()
        {
            InitializeComponent();
            initConfig();
        }

        // Função de configuração inicial da porta serial
        void InitSerialPort()
        {
            serialPort1.BaudRate = Convert.ToInt32(cbBaudRate.Items[cbBaudRate.SelectedIndex]);
            serialPort1.DataBits = Convert.ToInt32(cbDataBits.Items[cbDataBits.SelectedIndex]);
            serialPort1.RtsEnable = Convert.ToBoolean(cbRtsEnable.Items[cbRtsEnable.SelectedIndex]);
            serialPort1.Handshake = Handshake.None;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
        }

        // Função de configuração inicial do form
        void initConfig()
        {
            tmrCom1.Enabled = true;

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(TreatReceiverData);

            btnConnect.BackColor = Color.LightBlue;
            btnConnect.ForeColor = Color.Black;

            tmrCom1.Interval = 750;

            // Valor inicial do baudrate em 9600 bps
            foreach (string x in cbBaudRate.Items)
            {
                if (x.Equals("9600"))
                {
                    cbBaudRate.SelectedIndex = cbBaudRate.Items.IndexOf(x);
                }
            }

            // Valor inicial do tamanho do pacote de dados em 8 bits
            foreach (string x in cbDataBits.Items)
            {
                if (x.Equals("8"))
                {
                    cbDataBits.SelectedIndex = cbDataBits.Items.IndexOf(x);
                }
            }

            // Valor de inicial do RTS (Request To Send) em "true", ou seja, ativado 
            foreach (string x in cbRtsEnable.Items)
            {
                if (x.Equals("True"))
                {
                    cbRtsEnable.SelectedIndex = cbRtsEnable.Items.IndexOf(x);
                }
            }
        }

        // Método que permite receber os dados da serial e manipular os controles
        public void SetTextBox(String text)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { SetTextBox(text); });
                return;
            }
        }

        // Atualiza a lista de portas seriais disponíveis
        private void updateListComs()
        {
            int i = 0;
            bool differentAmount = false;

            if(cbPort1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach(string s in SerialPort.GetPortNames())
                {
                    if(cbPort1.Items[i++].Equals(s) == false)
                    {
                        differentAmount = true;
                    }
                }
            }
            else
            {
                differentAmount = true;
            }

            if(differentAmount == false)
            {
                return;
            }

            cbPort1.Items.Clear();

            foreach(string s in SerialPort.GetPortNames())
            {
                cbPort1.Items.Add(s);
            }

            cbPort1.SelectedIndex = 0;
        }

        // Método que habilita a comunicação com a porta serial 
        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (cbBaudRate.SelectedIndex == -1 || cbDataBits.SelectedIndex == -1 || cbRtsEnable.SelectedIndex == -1 )
            {
                DialogResult message = MessageBox.Show("Propriedades vazias!\n Por favor, inserir valores válidos",
                                                        "ATENÇÃO!",
                                                        MessageBoxButtons.OK, 
                                                        MessageBoxIcon.Exclamation); 
                return;
            }
            else
            {
                InitSerialPort();
                if (serialPort1.IsOpen == false)
                {
                    try
                    {
                        serialPort1.PortName = cbPort1.Items[cbPort1.SelectedIndex].ToString();
                        serialPort1.Open();
                    }
                    catch
                    {
                        serialPort1.Close();
                        return;
                    }

                    if (serialPort1.IsOpen)
                    {
                        btnConnect.Text      = "DISCONNECT";   
                        btnConnect.BackColor = Color.Blue;
                        btnConnect.ForeColor = Color.White;
                        cbPort1.Enabled      = false;
                        cbBaudRate.Enabled   = false;
                        cbDataBits.Enabled   = false;
                        cbRtsEnable.Enabled  = false;
                    }
                }
                else
                {
                    try
                    {
                        serialPort1.Close();
                        cbPort1.Enabled        = true;
                        cbBaudRate.Enabled     = true;
                        cbDataBits.Enabled     = true;
                        cbRtsEnable.Enabled    = true;  
                        btnConnect.BackColor   = Color.LightBlue;
                        btnConnect.ForeColor   = Color.Black;
                        btnConnect.Text        = "CONNECT";
                    }
                    catch
                    {
                        serialPort1.Close();
                        return;
                    }
                }
            }
        }

        // Desabilita a comunicação serial quando o "Form" é fechado
        private void frmInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(serialPort1.IsOpen == true)
            {
                //serialPort1.Close();
            }
        }

        // Método que recebe os dados da porta serial
        private void TreatReceiverData(object sender, EventArgs e)
        {
            try
            {
                rxString = serialPort1.ReadExisting();
                serialHeader = rxString.Substring(0, 6);

                if (serialHeader != "INPUT ")
                {
                    if (serialHeader == "OUTPUT")
                    {
                        serialString = rxString.Substring(0, 13);
                        SetTextBox(serialString);
                    }
                    else
                    {
                        serialString = rxString.Substring(0, lengthSerialString);
                        SetTextBox(serialString);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        // Gera eventos para atualizar a verificação das portas seriais disponíveis
        private void tmrCom1_Tick(object sender, EventArgs e)
        {
            updateListComs();
            try
            {
                if (serialPort1.IsOpen == true)
                    serialPort1.Write("INP");
            }
            catch
            {
                serialPort1.Close();
                return;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
                MessageBox.Show("Porta serial não configurada");

            this.Close();
        }

        private void btnFan2_Click(object sender, EventArgs e)
        {
            try
            {
                string sSerialWrite = (m_bFan2) ? "OUT11" : "OUT10";
                serialPort1.Write(sSerialWrite);
                m_bFan2 = !m_bFan2;
            }
            catch
            {
                serialPort1.Close();
                return;
            }
        }

        private void btnSmell_Click(object sender, EventArgs e)
        {
            try
            {
                string sSerialWrite = (m_bSmell) ? "OUT01" : "OUT00";
                serialPort1.Write(sSerialWrite);
                m_bSmell = !m_bSmell;
            }
            catch
            {
                serialPort1.Close();
                return;
            }
        }
    }
}
