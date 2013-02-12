using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsObjectsManager : MonoBehaviour
{
    public Texture InfoTexture;

    private PhysicsObject _currentPhysicsObject;

    private Rect _windowPosition = new Rect(10, 10, 200, Screen.height / 2);

    void OnGUI()
    {
        _windowPosition = GUI.Window(
            0,
            _windowPosition,
            DoPhysComponentsManagetWindowPosition,
            "Физические объекты");
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        if (_currentPhysicsObject != null)
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Текущий элемент: " + _currentPhysicsObject.Identifier);
        else
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Текущий элемент: ");

        if (GetPhysicsObjects() != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, _windowPosition.width - 20, 300), new Vector2(0, 100), new Rect(0, 0, _windowPosition.width - 20, 300));
            foreach (PhysicsObject component in GetPhysicsObjects())
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

    public void SetCurrentComponent(PhysicsObject physicsObject)
    {
        _currentPhysicsObject = physicsObject;

        Debug.Log(string.Format(
            "{0} - SetCurrentComponent => Set new current component with name <{1}> ",
            this.GetType(),
            physicsObject.Identifier
            ));
    }

    public static List<PhysicsObject> GetPhysicsObjects()
    {
        return FindObjectsOfType(typeof(PhysicsObject)).OfType<PhysicsObject>().ToList();
    }
}
