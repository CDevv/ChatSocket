using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Chat Server");
string msg = "";

string localhost = Dns.GetHostName();
IPHostEntry entry = Dns.GetHostEntry(localhost);
IPEndPoint endPoint = new(entry.AddressList[0], 11000);
Socket socket = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

try
{
    byte[] buffer = new byte[1024];
    socket.Bind(endPoint);
    socket.Listen(100);
    Socket handle = socket.Accept();
    while (true)
    {
        msg = "";
        while (true)
        {
            int msgSize = handle.Receive(buffer);
            msg = Encoding.ASCII.GetString(buffer, 0, msgSize);
            msg = msg.Replace("<EOF>", "");
            Console.WriteLine(">" + msg);
            if (msg == "exit")
            {
                handle.Shutdown(SocketShutdown.Both);
                handle.Close();
                break;
            }
        }
    }
}
catch (Exception e)
{
    throw;
    Console.WriteLine(e.Message);
}

Console.Clear();
Console.WriteLine("Goodbye");
