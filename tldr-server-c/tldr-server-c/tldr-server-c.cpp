#include "stdafx.h"
#include <iostream>
#include <string>

using namespace System;
using namespace System::Net;
using namespace System::Net::Sockets;
using namespace System::Net::NetworkInformation;
using namespace System::IO;
using namespace System::Text;
using namespace System::Data;
using namespace System::Threading;
using namespace System::Windows::Forms;
using namespace System::Collections::Generic;

public ref class Server
{
	public:

	static List<String^>^ sUsersList = gcnew List<String^>();
	static Random^ rnd = gcnew Random();
	static UdpClient^ udpServer = gcnew UdpClient();

	static String^ sPinCode = rnd->Next(1156465, 456465465).ToString();

	static void UpdateUsers()
	{
		while (1)
		{
			try
			{
				if (Server::sUsersList->Count > 0) {
					String^ strUsersOnline = String::Join("|", sUsersList->ToArray());
					array<String^>^ strUsersOnlineSplit = strUsersOnline->Split('|');

					String^ final = "";

					for each(String^ i in strUsersOnlineSplit)
					{
						array<String^>^ tempParam = i->Split(',');

						if (!final->Contains(tempParam[0]))
							final += tempParam[0] + Environment::NewLine;
					}

					Server::SendToAll(3, final, "#000");
				}
			}
			catch (Exception^) {}

			Thread::Sleep(1000);
		}
	}

	static void SendToSpecific(Int32^ index, String^ content, String^ name, String^ ip, int port, String^ color) {
		if (Server::sUsersList->Count < 0) {
			return;
		}

		try {
			String^ joint = String::Join("|", Server::sUsersList->ToArray());
			array<String^>^ strUsersJointSplit = joint->Split('|');

			String^ contentWithIndex = index->ToString() + "|" + content + "|" + color;

			Server::udpServer->Send(Encoding::ASCII->GetBytes(contentWithIndex), contentWithIndex->Length, ip, port);
		}
		catch (Exception^ ex) {
			Console::WriteLine(ex->Message);
		}
	}

	static void SendToAll(Int32^ index, String^ content, String^ color) {
		if (Server::sUsersList->Count < 0) {
			return;
		}

		try {
			String^ joint = String::Join("|", Server::sUsersList->ToArray());
			array<String^>^ strUsersJointSplit = joint->Split('|');

			for each (String^ i in strUsersJointSplit)
			{
				array<String^>^ tempParam = i->Split(',');

				String^ contentWithIndex = index->ToString() + "|" + content + "|" + color;
				Server::udpServer->Send(Encoding::ASCII->GetBytes(contentWithIndex), contentWithIndex->Length, tempParam[1], Int32::Parse(tempParam[2]));
			}
		}
		catch (Exception^) {}
	}

	static void ReceiveCallback(IAsyncResult^ asyncResult)
	{
		UdpClient^ c = (UdpClient^)asyncResult->AsyncState;
		IPEndPoint^ ip = gcnew IPEndPoint(IPAddress::Any, 0);

		try {
			array<Byte>^ receivedBytes = c->EndReceive(asyncResult, ip);

			String^ receivedText = Encoding::ASCII->GetString(receivedBytes);
			array<String^>^ receivedTextArg = receivedText->Split('|');

		
			if (!Server::sUsersList->Contains(receivedTextArg[1] + "," + ip->Address->ToString() + "," + ip->Port))
			{
				Server::sUsersList->Add(receivedTextArg[1] + "," + ip->Address->ToString() + "," + ip->Port);
			}

			if (receivedTextArg[0] == "2")
			{
				DateTime^ localtime = DateTime::Now;

				if (receivedTextArg[2]->StartsWith("/time"))
				{
					SendToSpecific(1, "Server Time: " + localtime->ToString() + Environment::NewLine, receivedTextArg[1], ip->Address->ToString(), ip->Port, "blue");
				}
				else if (receivedTextArg[2]->StartsWith("/clear") || receivedTextArg[2]->StartsWith("/cls"))
				{
					SendToSpecific(2, String::Empty, receivedTextArg[1], ip->Address->ToString(), ip->Port, "blue");
				}
				else
				{
					SendToAll(1, "[" + receivedTextArg[1] + "]: " + receivedTextArg[2] + Environment::NewLine, receivedTextArg[3]);
				}
				
			}
			else if (receivedTextArg[0] == "1")
			{
				SendToAll(1, "[" + receivedTextArg[1] + " has connected to the server.]" + Environment::NewLine, "#000");
				SendToSpecific(2, String::Empty, receivedTextArg[1], ip->Address->ToString(), ip->Port, "#00");
			}
			else if (receivedTextArg[0] == "0")
			{
				SendToAll(1, "[" + receivedTextArg[1] + " has disconnected to the server.]" + Environment::NewLine, "#000");

				Server::sUsersList->Remove(receivedTextArg[1] + "," + ip->Address->ToString() + "," + ip->Port);
			}

			c->BeginReceive(gcnew AsyncCallback(ReceiveCallback), asyncResult->AsyncState);
		}
		catch (Exception^)
		{
			c->BeginReceive(gcnew AsyncCallback(ReceiveCallback), asyncResult->AsyncState);
		}
	}
};

int main(array<String^>^ args)
{
	if (args->Length != 0 && args[0]->StartsWith("-key"))
		Server::sPinCode = args[1]->ToString();
	
	try { 
		Server::udpServer->Client->Bind(gcnew IPEndPoint(IPAddress::Any, 17855));
		Server::udpServer->BeginReceive(gcnew AsyncCallback(Server::ReceiveCallback), Server::udpServer);
	} catch (SocketException^) {
		Windows::Forms::MessageBox::Show("Sorry, port probably taken!", "Error!");
		Environment::Exit(0);
	}

	Console::BackgroundColor = ConsoleColor::White;

	Thread^ backgroundUpdateUsers = gcnew Thread(gcnew ThreadStart(Server::UpdateUsers));
	backgroundUpdateUsers->Start();

	while (1)
	{
		Console::ForegroundColor = ConsoleColor::Black;
		Console::WriteLine("TLDR Server - C++ Edition");

		if (args->Length == 0)
			Console::WriteLine("Pin Code: " + Server::sPinCode);

		Console::WriteLine();

		String^ joint = String::Join(Environment::NewLine, Server::sUsersList->ToArray());

		Console::WriteLine("Online Currently:" + Environment::NewLine + joint->ToString());

		Thread::Sleep(1000);
		Console::Clear();
	}
}






