using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tldr_server
{
    class Program
    {
        public static List<string> strUserList = new List<string>();
        public static List<string> strBannedList = new List<string>();
        public static List<string> strAdminList = new List<string>();

        public static Random rnd = new Random();

        public static string strPinCode = rnd.Next(1156465,456465465).ToString();

        public static string strChatContent;

        public static UdpClient udpServer = new UdpClient();

        static void Main(string[] args)
        {
            udpServer.Client.Bind(new IPEndPoint(IPAddress.Any, 17855));
            udpServer.BeginReceive(DataReceived, udpServer);

            UpdateUsers();
            Sync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TLDR Server v1.0.0 (c) vldrTechnologies");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Pin Code: " + strPinCode);
            Console.ResetColor();

            while (true)
            {
            }
        }

        private static async void UpdateUsers()
        {
            while (true)
            {
                if (strUserList.Any())
                {
                    string strUsersOnline = string.Join("|", strUserList.ToArray());
                    string[] strUsersOnlineSplit = strUsersOnline.Split('|');

                    string final = "";

                    foreach (string i in strUsersOnlineSplit)
                    {
                        string[] tempParam = i.Split(',');

                        if (!final.Contains(tempParam[0]))
                            final += tempParam[0] + Environment.NewLine;
                    }

                    SendToAll(3, final);
                }

                strUserList.Clear();
                await Task.Delay(1000);
            }
        }

        private static async void Sync()
        {
            while (true)
            {
                strUserList.Clear();
                await Task.Delay(15000);
            }
        }

        private static void SendToAll(int index, string content, string color = "#000")
        {
            if (!strUserList.Any()) { return; }

            string strUsersJoint = string.Join("|", strUserList.ToArray());
            string[] strUsersJointSplit = strUsersJoint.Split('|');

            foreach (string i in strUsersJointSplit)
            {
                string[] tempParam = i.Split(',');

                string contentWithIndex = index.ToString() + "|" + content + "|" + color;

                udpServer.Send(Encoding.ASCII.GetBytes(contentWithIndex), contentWithIndex.Length, tempParam[1], Int32.Parse(tempParam[2]));
            }
        }

        private static void SendToSpecific(int index, string content, string name, string ip, int port, string color = "#000")
        {
            if (!strUserList.Any()) { return; }

            string strUsersJoint = string.Join("|", strUserList.ToArray());
            string[] strUsersJointSplit = strUsersJoint.Split('|');

            foreach (string i in strUsersJointSplit)
            {
                string[] tempParam = i.Split(',');

                string contentWithIndex = index.ToString() + "|" + content + "|" + color;

                if (tempParam[0] == name && tempParam[1] == ip && Int32.Parse(tempParam[2]) == port)
                    udpServer.Send(Encoding.ASCII.GetBytes(contentWithIndex), contentWithIndex.Length, tempParam[1], Int32.Parse(tempParam[2]));
            }
        }

        private static void DataReceived(IAsyncResult ar)
        {
            UdpClient c = (UdpClient)ar.AsyncState;

            try
            {
                IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                Byte[] receivedBytes = c.EndReceive(ar, ref receivedIpEndPoint);

                string receivedText = System.Text.Encoding.ASCII.GetString(receivedBytes);
                string[] receivedTextArg = receivedText.Split('|');

                if (!strUserList.Contains(receivedTextArg[1] + "," + receivedIpEndPoint.Address.ToString() + "," + receivedIpEndPoint.Port))
                {
                    if (!strBannedList.Contains(receivedIpEndPoint.Address.ToString()))
                        strUserList.Add(receivedTextArg[1] + "," + receivedIpEndPoint.Address.ToString() + "," + receivedIpEndPoint.Port);
                    else
                        goto END;
                }

                strUserList.Sort();

                if (receivedTextArg[0] == "2")
                {
                    if (receivedTextArg[2].Contains("/ban"))
                    {
                        try
                        {
                            if (!strAdminList.Contains(receivedIpEndPoint.Address.ToString()))
                            {
                                SendToSpecific(1, "[Insufficient Permissions!]" + Environment.NewLine, receivedTextArg[1],
                                    receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                                goto END;
                            }

                            string strUsers = string.Join("|", strUserList.ToArray());
                            string[] strUsersSplit = strUsers.Split('|');

                            foreach (string i in strUsersSplit)
                            {
                                string[] tempParam = i.Split(',');

                                if (tempParam[0].ToLower() == receivedTextArg[2].Remove(0, 5).ToLower())
                                {
                                    string foundUser = Array.Find(strUsersSplit, element => element.Contains(receivedTextArg[2].Remove(0, 5)));
                                    string[] foundUserSplit = foundUser.Split(',');

                                    strBannedList.Add(foundUserSplit[1]);
                                    strUserList.Remove(foundUserSplit[1]);

                                    SendToAll(1, "[" + foundUserSplit[0] + " has been banned from the server.]" + Environment.NewLine, "darkblue");
                                }
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            SendToSpecific(1, "[Incorrect parameters!]" + Environment.NewLine, receivedTextArg[1],
                                receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                        }   
                    }
                    else if (receivedTextArg[2].Contains("/admin"))
                    {
                        try
                        {
                            if (receivedTextArg[2].Remove(0, 7) == strPinCode && !strAdminList.Contains(receivedIpEndPoint.Address.ToString()))
                            {
                                strAdminList.Add(receivedIpEndPoint.Address.ToString());

                                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                                var random = new Random();

                                strPinCode = Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray().ToString();
                                Console.WriteLine(strPinCode.ToString());

                                SendToSpecific(1, "[You have been added as an administrator!]" + Environment.NewLine, receivedTextArg[1],
                                    receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkblue");
                            }
                            else
                            {
                                SendToSpecific(1, "[Insufficient Permissions!]" + Environment.NewLine, receivedTextArg[1],
                                    receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            SendToSpecific(1, "[Incorrect parameters!]" + Environment.NewLine, receivedTextArg[1],
                                receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                        }
                        
                    }
                    else if (receivedTextArg[2].Contains("/cls"))
                    {
                        if (strAdminList.Contains(receivedIpEndPoint.Address.ToString()))
                        {
                            strChatContent = string.Empty;
                            Console.Clear();

                            SendToAll(2, strChatContent);
                        }
                        else
                        {
                            SendToSpecific(1, "[Insufficient Permissions!]" + Environment.NewLine, receivedTextArg[1],
                                receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                        }
                    }
                    else if (receivedTextArg[2].Contains("/a"))
                    {
                        string strUsers = string.Join("|", strAdminList.ToArray());
                        string[] strUsersSplit = strUsers.Split('|');

                        string ans = "Admins: " +   Environment.NewLine;

                        foreach (string i in strUsersSplit)
                        {
                            ans += i + Environment.NewLine;
                        }

                        SendToSpecific(1, ans + Environment.NewLine, receivedTextArg[1],
                                 receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port, "darkred");
                    }
                    else
                    {
                        SendToAll(1, "[" + receivedTextArg[1] + "]: " + receivedTextArg[2] + Environment.NewLine, receivedTextArg[3]);

                        strChatContent += "[" + receivedTextArg[1] + "]: " + receivedTextArg[2] + Environment.NewLine;
                        Console.WriteLine("[" + receivedTextArg[1] + "]: " + receivedTextArg[2] + Environment.NewLine);
                    }
                }
                else if (receivedTextArg[0] == "1")
                {
                    SendToAll(1, "[" + receivedTextArg[1] + " has connected to the server.]" + Environment.NewLine);
                    SendToSpecific(2, string.Empty, receivedTextArg[1], receivedIpEndPoint.Address.ToString(), receivedIpEndPoint.Port);

                    strChatContent += "[" + receivedTextArg[1] + " has connected to the server.]" + Environment.NewLine;
                    Console.WriteLine("[" + receivedTextArg[1] + " has connected to the server.]" + Environment.NewLine);
                }
                else if (receivedTextArg[0] == "0")
                {
                    strUserList.Remove(receivedTextArg[1] + "," + receivedIpEndPoint.Address.ToString() + "," + receivedIpEndPoint.Port);

                    SendToAll(1, "[" + receivedTextArg[1] + " has disconnected from the server.]" + Environment.NewLine);

                    strChatContent += "[" + receivedTextArg[1] + " has disconnected from the server.]" + Environment.NewLine;
                    Console.WriteLine("[" + receivedTextArg[1] + " has disconnected from the server.]" + Environment.NewLine);
                }

            END:
                c.BeginReceive(DataReceived, ar.AsyncState);
            }
            catch (Exception)
            {
                strUserList.Clear();
                c.BeginReceive(DataReceived, ar.AsyncState);
            }
        }
    }
}
