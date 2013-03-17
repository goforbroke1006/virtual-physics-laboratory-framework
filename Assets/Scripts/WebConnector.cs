using UnityEngine;

public class WebConnector : MonoBehaviour
{
    public void JsExternalCall(string data)
    {
        //BeanManager.GetOutputConsole().AddMessage("Send: " + data);
        if (data.Length > 0)
            Application.ExternalCall("MapleCalculate", data);
    }

    public void UnityCall(string data)
    {
        //BeanManager.GetOutputConsole().AddMessage("Received info: \n" + data);
        BeanManager.GetLabPlayer().Response = data;
    }
}
