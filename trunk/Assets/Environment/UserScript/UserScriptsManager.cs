using UnityEngine;
using System.Collections;

public class UserScriptsManager : MonoBehaviour
{
    private Rect _windowPosition = new Rect(Screen.width - 10 - 250, 10, 250, 350);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        _windowPosition = GUI.Window(
            1,
            _windowPosition,
            DoPhysComponentsManagetWindowPosition,
            "Behaviors");
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        GUI.DragWindow();
    }
}
