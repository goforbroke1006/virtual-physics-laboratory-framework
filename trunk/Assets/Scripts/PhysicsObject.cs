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
            BeanManager.GetPhysicsObjectsManager().SetCurrentObject(this);
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

    public BasicPhysicsProperty GetProperty(string propName)
    {
        return GetProperties().FirstOrDefault(property => property.GetName() == propName);
    }
}
