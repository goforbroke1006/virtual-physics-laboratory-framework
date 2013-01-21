using System.Globalization;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class WebConnector : MonoBehaviour
{
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

    public string Calculate(string code)
    {
        _packagesCounter++;

        // send code to Maple with Sockets

        return "";
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
