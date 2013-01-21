using System.Globalization;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private WebConnector _webConnector;
    private bool _isPlay = false;

    // Use this for initialization
    void Start()
    {
        _webConnector = (WebConnector) FindObjectOfType(typeof (WebConnector));
    }

    void Update()
    {
        if (_isPlay && _webConnector != null)
        {
            foreach (UserScript script in FindObjectsOfType(typeof(UserScript)).OfType<UserScript>().ToList())
            {
                string result = _webConnector.Calculate(script.Code);
                // Apply result
            }
        }
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height - 105, 300, 100));
        GUI.Box(new Rect(0, 0, 300, 100), "Lab player");

        if (GUI.Button(new Rect(10, 25, 80, 24), "Pause"))
            _isPlay = false;

        if (GUI.Button(new Rect(100, 25, 100, 24), "Play"))
            _isPlay = true;

        if (GUI.Button(new Rect(210, 25, 80, 24), "Reset"))
        {
            _isPlay = false;
            foreach (PhysObject physObject in FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList())
                physObject.Reset();
        }

        GUI.EndGroup();
    }
}
