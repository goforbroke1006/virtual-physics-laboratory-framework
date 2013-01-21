using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class PhysObject : MonoBehaviour
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

    public IProperty GetProperty(string name)
    {
        List<IProperty> list = FindObjectsOfType(typeof(IProperty)).OfType<IProperty>().ToList();
        foreach (IProperty property in list)
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
            ((PhysObjectsManager)FindObjectOfType(typeof(PhysObjectsManager))).SetCurrentComponent(this);
        }
        catch (Exception exception)
        {
            Debug.LogError("PhysicsObjectBehaviour - OnMouseOver() - " + this.name + " - " + exception.Message);
        }
        //((EnvironmentManager)transform.parent.gameObject.GetComponent<EnvironmentManager>()).AddElement(this.gameObject);
        //GetComponent<EnvironmentManager>().AddElement(this.gameObject);

    }

    void OnSceneGUI()
    {
        Handles.PositionHandle(new Vector3(), Quaternion.identity);
    }
}
