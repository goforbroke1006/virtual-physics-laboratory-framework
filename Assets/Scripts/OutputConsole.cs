using UnityEngine;
using System.Collections;

public class OutputConsole : MonoBehaviour
{
    public bool Visible;

    private Rect _windowPosition = new Rect(10, Screen.height / 4 * 3, Screen.width - 50, Screen.height / 4);
    private Vector2 _scroll;// = new Vector2(100, 0);
    private string _content = "";
    
    public void AddMessage(string message)
    {
        if (message.Length > 0)
            _content += "\n" + message;
    }

    void OnGUI()
    {
        if (Visible)
        {
            _windowPosition = GUI.Window(
                4,
                _windowPosition,
                DoWindow,
                "Консоль");
        }
    }

    void DoWindow(int id)
    {
        _scroll = GUILayout.BeginScrollView(_scroll);
        GUI.TextArea(new Rect(10, 30, _windowPosition.width - 80, _windowPosition.height - 60), _content);
        GUILayout.EndScrollView();

        GUI.DragWindow();
    }
}
