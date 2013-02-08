using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysObjectsManager : MonoBehaviour
{
    public Texture InfoTexture;

    public static List<PhysObject> PhysObjects;
    private PhysObject _currentPhysObject;

    private Rect _windowPosition = new Rect(10, 10, 200, Screen.height / 2);

    // Use this for initialization
    void Start()
    {
        PhysObjects = FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList();
        //gameObject.transform.parent.gameObject.GetComponentsInChildren<PhysObject>().OfType<PhysObject>().ToList();
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
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Current phys component: " + _currentPhysObject.Identifier);
        else
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Current phys component: ");

        if (PhysObjects != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, _windowPosition.width - 20, 300), new Vector2(0, 100), new Rect(0, 0, _windowPosition.width - 20, 300));
            foreach (PhysObject component in PhysObjects)
            {
                if (GUI.Button(new Rect(0, counter * 24, _windowPosition.width - 20, 24), component.Identifier))
                    this.SetCurrentComponent(component);
                counter++;
            }
            GUI.EndScrollView();
        }
        else
            Debug.Log("Has not Phys Components");

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
