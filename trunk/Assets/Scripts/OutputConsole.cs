using UnityEngine;
using System.Collections;

public class OutputConsole : MonoBehaviour
{
    private Rect _windowPosition = new Rect(10, Screen.height / 4 * 3, Screen.width - 50, Screen.height / 4);
    private Vector2 scroll;// = new Vector2(100, 0);
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
                "Консоль");
        }
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        scroll = GUILayout.BeginScrollView(scroll);
        if (Content.Length > 0)
            GUI.TextArea(new Rect(10, 30, _windowPosition.width - 100, _windowPosition.height - 40), Content);
        GUILayout.EndScrollView();

        GUI.DragWindow();
    }
}
