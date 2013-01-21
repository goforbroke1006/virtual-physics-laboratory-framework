using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class PhysObjectsManager : MonoBehaviour
{
    public Texture InfoTexture;

    private List<PhysObject> _physObjects;
    private PhysObject _currentPhysObject;

    private Rect _windowPosition = new Rect(10, 10, 250, 350);

    // Use this for initialization
    void Start()
    {
        _physObjects = GetComponentsInChildren<PhysObject>().OfType<PhysObject>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        _windowPosition = GUI.Window(
            0,
            _windowPosition,
            DoPhysComponentsManagetWindowPosition,
            "Phys Components");
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        if (_currentPhysObject != null)
            GUI.Label(new Rect(10, 30, 230, 24), "Current phys component: " + _currentPhysObject.Identifier);
        else
            GUI.Label(new Rect(10, 30, 230, 24), "Current phys component: ____");
        
        if (_physObjects != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, 230, 300), new Vector2(0, 100), new Rect(0, 0, 230, 300));
            foreach (PhysObject component in _physObjects)
            {
                if (GUI.Button(new Rect(0, counter * 24, 230, 24), component.Identifier))
                    this.SetCurrentComponent(component);
                counter++;
            }
            GUI.EndScrollView();
        }
        else
            Debug.Log("Has not Phys Components");

//        GUI.DrawTexture(new Rect(10, 220, 20, 20), new Texture());

        GUI.DrawTexture(new Rect(40, 220, 20, 20), InfoTexture);
        GUI.Button(new Rect(40, 220, 20, 20), "");

        GUI.DragWindow();
    }

    public void SetCurrentComponent(PhysObject physObject)
    {
        _currentPhysObject = physObject;

        Debug.Log(string.Format(
            "{0} - SetCurrentComponent => Set new current component with name <{1}> ",
            this.GetType(),
            physObject.Identifier
            ));
    }
}
