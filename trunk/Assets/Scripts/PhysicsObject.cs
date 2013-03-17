using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public string Identifier;

    void OnMouseDown()
    {
        try
        {
            ((PhysicsObjectsManager)FindObjectOfType(typeof(PhysicsObjectsManager))).SetCurrentObject(this);
        }
        catch (Exception exception)
        {
            Debug.LogError("PhysicsObjectBehaviour - OnMouseOver() - " + Identifier + " - " + exception.Message);
        }
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
}
