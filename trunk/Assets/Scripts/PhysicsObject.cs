using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public string Identifier;
    private Transform _startTransform;

    // Use this for initialization
    void Start()
    {
        _startTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<BasicPhysicsProperty> GetProperties()
    {
        return GetComponents<BasicPhysicsProperty>().OfType<BasicPhysicsProperty>().ToList();
    }

    public BasicPhysicsProperty GetProperty(string name)
    {
        List<BasicPhysicsProperty> list = GetProperties();
        foreach (BasicPhysicsProperty property in list)
        {
            if (property.GetName() == name)
                return property;
        }
        return null;
    }

    public void Reset()
    {
        gameObject.transform.position = _startTransform.position;
        gameObject.transform.rotation = _startTransform.rotation;
        gameObject.transform.localScale = _startTransform.localScale;
    }

    void OnMouseDown()
    {
        try
        {
            ((PhysicsObjectsManager)FindObjectOfType(typeof(PhysicsObjectsManager))).SetCurrentObject(this);
        }
        catch (Exception exception)
        {
            Debug.LogError("PhysicsObjectBehaviour - OnMouseOver() - " + this.name + " - " + exception.Message);
        }
        //((EnvironmentManager)transform.parent.gameObject.GetComponent<EnvironmentManager>()).AddElement(this.gameObject);
        //GetComponent<EnvironmentManager>().AddElement(this.gameObject);

    }

    /*void OnSceneGUI()
    {
        Handles.PositionHandle(new Vector3(), Quaternion.identity);
    }*/
}
