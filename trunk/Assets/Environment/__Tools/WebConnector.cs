using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class WebConnector : MonoBehaviour
{
    private TcpClient _tcpClient = null;
    private string _ipAddress = string.Empty;
    private string _port = string.Empty;
    private int _packagesCounter = 0;

    // Use this for initialization
    void Start()
    {
        // send request for get ip|port
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width - 205, Screen.height - 105, 200, 100));
        GUI.Box(new Rect(0, 0, 200, 100), "Web Connector");
        GUI.Label(new Rect(10, 25, 70, 23), "Ip address:");
        GUI.Label(new Rect(80, 25, 120, 23), _ipAddress);
        GUI.Label(new Rect(10, 45, 70, 23), "Port:");
        GUI.Label(new Rect(80, 45, 120, 23), _port);
        GUI.Label(new Rect(10, 65, 70, 23), "Packages count:");
        GUI.Label(new Rect(80, 65, 120, 23), _packagesCounter.ToString(CultureInfo.InvariantCulture));
        GUI.EndGroup();
    }

    /*public string Calculate(string code)
    {
        if (_tcpClient == null)
            if (_ipAddress.Length > 0 && _port.Length > 0)
                _tcpClient = new TcpClient(_ipAddress, Convert.ToInt32(_port));


        StreamWriter streamWriter = new StreamWriter(_tcpClient.GetStream());
        streamWriter.WriteLine(code);
        streamWriter.Flush();
        _packagesCounter++;

        StreamReader streamReader = new StreamReader(_tcpClient.GetStream());
        return streamReader.ReadLine();
    }*/

    private string message;

    public string Calculate(string code)
    {
        message = "Calculate";
        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(_ipAddress), Convert.ToInt32(_port));
        Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(ipe);
        message = "Connecting";
        socket.Send(Encoding.UTF8.GetBytes(code), Encoding.UTF8.GetBytes(code).Length, 0);
        message = "Send";

        byte[] bytesReceived = new byte[1024];
        int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
        message = "Received data";
        return Encoding.UTF8.GetString(bytesReceived, 0, bytes);
    }

    string GetIpAddress()
    {
        return _ipAddress;
    }

    void SetIpAddress(string ipAddress)
    {
        _ipAddress = ipAddress;
    }

    string GetPort()
    {
        return _port;
    }

    void SetPort(string port)
    {
        _port = port;
    }

}
