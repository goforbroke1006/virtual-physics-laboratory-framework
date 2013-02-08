using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using MathWorks;

public class Player : MonoBehaviour
{
    private bool _isPlay = false;

    private WebConnector _webConnector;

    MapleBuilder _mapleBuilder = new MapleBuilder();
    MapleParser _mapleParser = new MapleParser();

    // Use this for initialization
    void Start()
    {
        _webConnector = (WebConnector)FindObjectOfType(typeof(WebConnector));
    }

    void Update()
    {
        try
        {
            if (_isPlay)
                foreach (UserScript script in FindObjectsOfType(typeof(UserScript)).OfType<UserScript>().ToList())
                {
                    //_webConnector.ExternallCall(script.Code);
                }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height - 105, 300, 100));
        GUI.Box(new Rect(0, 0, 300, 100), "Lab player");

        if (GUI.Button(new Rect(10, 25, 80, 24), "Pause"))
            _isPlay = false;

        if (GUI.Button(new Rect(100, 25, 100, 24), "Play"))
        {
            foreach (PhysObject physObject in PhysObjectsManager.PhysObjects)
            {
                List<AbstractProperty> properties = physObject.gameObject.GetComponents<AbstractProperty>().OfType<AbstractProperty>().ToList();
                foreach (AbstractProperty property in properties)
                    _webConnector.ExternallCall(
                        _mapleBuilder.GetDefineVariableCode(physObject.Identifier, property.GetName(), property.GetValue())
                        );
            }

            _isPlay = true;
        }

        if (GUI.Button(new Rect(210, 25, 80, 24), "Reset"))
        {
            _isPlay = false;
            foreach (PhysObject physObject in FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList())
                physObject.Reset();
        }

        GUI.EndGroup();
    }
}
