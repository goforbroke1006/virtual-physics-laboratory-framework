using UnityEngine;
using System.Collections;

public class OutputConsole : MonoBehaviour
{
    private Rect _windowPosition = new Rect(10, Screen.height / 4 * 3, Screen.width - 20, Screen.height / 4);
    private string Content = "";
    public bool Visible;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMessage(string message)
    {
        if (message.Length > 0)
            Content += "\n" + message;
    }

    void OnGUI()
    {
        if (Visible)
        {
            _windowPosition = GUI.Window(
                4,
                _windowPosition,
                DoPhysComponentsManagetWindowPosition,
                "Console");
        }
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        if (Content.Length > 0)
            GUI.TextArea(new Rect(10, 30, _windowPosition.width - 20, _windowPosition.height - 40), Content);

        GUI.DragWindow();
    }
}
