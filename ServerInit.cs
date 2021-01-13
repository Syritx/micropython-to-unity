using UnityEngine;

class ServerInit : MonoBehaviour {

    TCPServer server;

    void Awake() {
        server = new TCPServer(this);
        server.Run();
        print(server.GetCurrentStatus());
    }

    public void Connection() {
        print("connection");
    }

    public void SendServerStatus(string msg) {
        print(msg);
    }
}