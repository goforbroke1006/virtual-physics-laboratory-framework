using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class UserScriptsManager : MonoBehaviour
{
    public Texture InfoTexture;

    private List<UserScript> _userScripts = new List<UserScript>();
    private Rect _windowPosition = new Rect(Screen.width - 10 - 200, 10, 200, Screen.height / 2);
    private UserScript _currentUserScript = null;

    // Use this for initialization
    void Start()
    {
        _userScripts = FindObjectsOfType(typeof(UserScript)).OfType<UserScript>().ToList();
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
        if (_currentUserScript != null)
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Current user script: " + _currentUserScript.Name);
        else
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Current user script: ");

        if (_userScripts != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, _windowPosition.width - 20, 300), new Vector2(0, 100), new Rect(0, 0, _windowPosition.width - 20, 300));
            foreach (UserScript script in _userScripts)
            {
                if (GUI.Button(new Rect(0, counter * 24, _windowPosition.width - 20, 24), script.Name))
                    this.SetCurrentUserScript(script);
                counter++;
            }
            GUI.EndScrollView();
        }

        // Draw INFO button
        GUI.DrawTexture(new Rect(10, _windowPosition.height - 40, 30, 30), InfoTexture);
        if (GUI.Button(new Rect(10, _windowPosition.height - 40, 30, 30), ""))
        {
            // TODO: show inforamtion about current Phys-Object
        }

        // Draw EDIT button
        GUI.DrawTexture(new Rect(45, _windowPosition.height - 40, 30, 30), InfoTexture);
        if (GUI.Button(new Rect(45, _windowPosition.height - 40, 30, 30), ""))
        {
            // TODO: show window for edit properties of current Phys-Object
        }

        GUI.DragWindow();
    }

    void SetCurrentUserScript(UserScript userScript)
    {
        _currentUserScript = userScript;
    }
}
