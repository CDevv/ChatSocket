using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Chat Client");
string msg = "";

string localhost = Dns.GetHostName();
IPHostEntry entry = Dns.GetHostEntry(localhost);
IPEndPoint endPoint = new(entry.AddressList[0], 11000);
Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

try
{
    socket.Connect(endPoint);
    while (true)
    {
        Console.Write(">");
        msg = Console.ReadLine();
        byte[] msgBytes = Encoding.ASCII.GetBytes(msg + "<EOF>");
        socket.Send(msgBytes);
        if (msg == "exit")
        {
            break;
        }
    }
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
