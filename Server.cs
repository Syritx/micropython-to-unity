using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System;

using UnityEngine;

class TCPServer {

    public enum Status {Running, Static};
    public Status status = Status.Static;
    Socket server;
    ServerInit init;

    IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

    public TCPServer(ServerInit init) {
        this.init = init;
    }

    public void Run() {
        status = Status.Running;
        server = new Socket(AddressFamily.InterNetwork, 
                            SocketType.Stream, 
                            ProtocolType.Tcp);

        IPEndPoint iPEndPoint = new IPEndPoint(ip, 5000);
        server.Bind(iPEndPoint);
        server.Listen(5);

        Task.Run(() => ReceiveClients());
    }

    void ClientHandlerThread(Socket client) {
        init.Connection();

        while (client.Connected) {
            byte[] bytes = new byte[2048];
            int i = client.Receive(bytes);

            string command = Encoding.UTF8.GetString(bytes);
            init.SendServerStatus(command);
        }
    }

    void ReceiveClients() {
        while (true) {
            //GameObject.Find("")
            Socket client = server.Accept();
            Task.Run(() => ClientHandlerThread(client));
        }
    }

    public Status GetCurrentStatus() {
        return status;
    }
}