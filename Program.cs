using System;
using System.Net.Sockets;
using System.Text;
namespace ConsoleApp5
{
	class ProgramГЕА
	{
		static void Main(string[] args)
		{
			using (Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				try
				{
					socket.Connect("help.websiteos.com", 80);
					var query = "GET / HTTP/1.1\r\nHost: help.websiteos.com\r\n\r\n";
					socket.Send(Encoding.ASCII.GetBytes(query));
					Span<byte> response = stackalloc byte[1000];
					socket.Receive(response);
					string responseAsString = Encoding.ASCII.GetString(response);
					var end = "/html>";
					var index = responseAsString.IndexOf(end);
					Console.WriteLine($"запрос\n{query}ответ\n{(index == -1 ? string.Empty : responseAsString.Substring(0, index + (end.Length * 2)))}");
				}
				finally
				{
					socket.Disconnect(false);
					socket.Dispose();
				}
			}
		}
	}
}
