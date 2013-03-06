using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsObjectsManager : MonoBehaviour
{
    public Material LineMaterial;

    private bool isShowed = true;

    private PhysicsObject _currentPhysicsObject;

    private Rect _windowPosition = new Rect(10, 10, 200, Screen.height / 2);

    void OnGUI()
    {
        _windowPosition = GUI.Window(0, _windowPosition, DoWindow, "Физические объекты");
    }

    void DoWindow(int id)
    {
        if (GUI.Button(new Rect(2, 2, 23, 23), "x"))
        {
            isShowed = !isShowed;

            if (isShowed) 
                _windowPosition.height = Screen.height/2;
            else 
                _windowPosition.height = 50;
        }

        if (_currentPhysicsObject != null)
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Тек. элемент: " + _currentPhysicsObject.Identifier);
        else
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Тек. элемент: ");

        if (GetPhysicsObjects() != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, _windowPosition.width - 20, 300), new Vector2(0, 100), new Rect(0, 0, _windowPosition.width - 20, 300));
            foreach (PhysicsObject component in GetPhysicsObjects())
            {
                if (GUI.Button(new Rect(0, counter * 24, _windowPosition.width - 20, 24), component.Identifier))
                    this.SetCurrentObject(component);
                counter++;
            }
            GUI.EndScrollView();
        }
        else
            Debug.Log("Has not Phys Components");

        GUI.DragWindow();
    }

    public PhysicsObject GetCurrectObject()
    {
        return this._currentPhysicsObject;
    }

    public void SetCurrentObject(PhysicsObject physicsObject)
    {
        _currentPhysicsObject = physicsObject;

        ((MatematicsModelView) FindObjectOfType(typeof (MatematicsModelView))).CurrentPhysicsObject =
            _currentPhysicsObject;

        Debug.Log(string.Format(
            "{0} - SetCurrentObject => Set new current component with name <{1}> ",
            this.GetType(),
            physicsObject.Identifier
            ));
    }

    public static List<PhysicsObject> GetPhysicsObjects()
    {
        return FindObjectsOfType(typeof(PhysicsObject)).OfType<PhysicsObject>().ToList();
    }
}
