using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tldr_client
{
    public partial class MainWindow : Window
    {
        public string strName;
        public string strIP;
        public string strOnline;
        public string strColor;

        public UdpClient udpClient = new UdpClient();

        public MainWindow(string name, string hexcolor, string ip)
        {
            InitializeComponent();

            strIP = ip;
            strName = name;
            strColor = hexcolor;

            lblTitle.Content += " (" + ip + ") ";

            startClient();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 
        }

        void OnProcessExit(object sender, EventArgs e)
        {
            sendInformation(0, strName, string.Empty);
        }

        private async void startClient()
        {
            try
            {
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom(strColor);
                
                udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
                udpClient.BeginReceive(dataReceived, udpClient);

                gridHeader.Background = brush;
                border.BorderBrush = brush;

                sendInformation(1, strName, string.Empty);
                while (true)
                {
                    sendInformation(3, strName, string.Empty);
                    await Task.Delay(100);
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Failed to connect to master server!");
                Environment.Exit(0);
            }
        }

        private void SendMessage()
        {
            if (txtInput.Text == string.Empty) { return; }
            if (txtInput.Text.Contains("|")) { MessageBox.Show("Can't send a message containing an incorrect symbol!"); return; }

            try
            {
                sendInformation(2, strName, txtInput.Text, strColor);
                txtInput.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Failed to send packets to master server!" + ex.Message);
            }
        }

        private void sendInformation(int dataType, string name, string content, string color = "#000")
        {
            string msg = dataType + "|" + name + "|" + content + "|" + color;
            udpClient.Send(Encoding.ASCII.GetBytes(msg),
                msg.Length,
                //"198.12.72.39",
                strIP,
                17855);
        }

        private void dataReceived(IAsyncResult ar)
        {
            try
            {
                UdpClient c = (UdpClient)ar.AsyncState;

                IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                Byte[] receivedBytes = c.EndReceive(ar, ref receivedIpEndPoint);
                string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
                string[] receivedTextArg = receivedText.Split('|');

                this.Dispatcher.Invoke((Action)(() =>
                {
                    if (receivedTextArg[0] == "1")
                        AppendText(richTxtContent, receivedTextArg[1], receivedTextArg[2]);
                    else if (receivedTextArg[0] == "2") {
                        richTxtContent.Document.Blocks.Clear();
                        richTxtContent.Document.Blocks.Add(new Paragraph(new Run(receivedTextArg[1])));
                    }               
                    else if (receivedTextArg[0] == "3")
                        txtOnlineUsers.Text = receivedTextArg[1];
                }));

                c.BeginReceive(dataReceived, ar.AsyncState);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to get data from master server!");
                Environment.Exit(0);
            }
        }

        private void AppendText(RichTextBox box, string text, string color)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            try 
            { 
                tr.ApplyPropertyValue(TextElement.ForegroundProperty,
                    (Brush)bc.ConvertFrom(color)); 
            }
            catch (FormatException) { }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }catch (Exception){}
        }

        private void lblExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void richTxtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            richTxtContent.ScrollToEnd();
        }
    }
}