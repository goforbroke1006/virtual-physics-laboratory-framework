using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsObjectsManager : MonoBehaviour
{
    public Texture InfoTexture;
    public Material LineMaterial;

    private PhysicsObject _currentPhysicsObject;

    private Rect _windowPosition = new Rect(10, 10, 200, Screen.height / 2);

    void OnGUI()
    {
        _windowPosition = GUI.Window(
            0,
            _windowPosition,
            DoPhysComponentsManagetWindowPosition,
            "Физические объекты");

        draw();
    }

    //void OnDrawGizmos()
    //{
    //    try
    //    {
    //        Gizmos.color = new Color(0f, 0f, 0f);
    //        PhysicsObject po = GetCurrectObject();
    //        Gizmos.DrawLine(po.transform.position, po.transform.position + new Vector3(100, 0, 0));
    //        Gizmos.DrawLine(po.transform.position, po.transform.position + new Vector3(0, 100, 0));
    //        Gizmos.DrawLine(po.transform.position, po.transform.position + new Vector3(0, 0, 100));

    //        //Handle.
    //    }
    //    catch (Exception exception)
    //    {
    //        Debug.LogWarning(exception.Message);
    //    }
    //}

    void draw()
    {
        PhysicsObject po = GetCurrectObject();

        //GL.PushMatrix();
        //LineMaterial.SetPass(0);
        //GL.LoadOrtho();
        //GL.Begin(GL.LINES);
        //GL.Color(Color.red);
        //GL.Vertex(po.transform.position);
        //GL.Vertex(po.transform.position + new Vector3(100, 0, 0));
        //GL.End();
        //GL.PopMatrix();

        if (po != null)
        {
            LineMaterial.SetPass(0);
            GL.Begin(GL.LINES);

            GL.Color(new Color(1f, 0f, 0f));
            GL.Vertex3(po.transform.position.x, po.transform.position.y, po.transform.position.z);
            GL.Vertex3(po.transform.position.x + 1, po.transform.position.y, po.transform.position.z);
            GL.End();

            GL.Color(new Color(0f, 1f, 0f));
            GL.Vertex3(po.transform.position.x, po.transform.position.y, po.transform.position.z);
            GL.Vertex3(po.transform.position.x, po.transform.position.y+1, po.transform.position.z);
            GL.End();

            GL.Color(new Color(0f, 0f, 1f));
            GL.Vertex3(po.transform.position.x, po.transform.position.y, po.transform.position.z);
            GL.Vertex3(po.transform.position.x, po.transform.position.y, po.transform.position.z+1);
            GL.End();

            //GL.Vertex3(0, 0, 0);
            //GL.Vertex3(1, 0, 0);
            //GL.Vertex3(0, 1, 0);
            //GL.Vertex3(1, 1, 0);
            //GL.Color(new Color(0f, 0f, 0f, 0.5f));
            //GL.Vertex3(0, 0, 0);
            //GL.Vertex3(0, 1, 0);
            //GL.Vertex3(1, 0, 0);
            //GL.Vertex3(1, 1, 0);
            //GL.End();
        }
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
                    this.SetCurrentObject(component);
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
