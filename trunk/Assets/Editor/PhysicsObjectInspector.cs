/*
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PhysicsObject))]
public class PhysicsObjectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        try
        {
            PhysicsObject physicsObject = (PhysicsObject)target;

            physicsObject.Identifier = EditorGUILayout.TextField("Идентификатор", physicsObject.Identifier);

            List<BasicPhysicsProperty> properties = physicsObject.GetComponents<BasicPhysicsProperty>().OfType<BasicPhysicsProperty>().ToList();
            foreach (BasicPhysicsProperty property in properties)
            {
                string value = EditorGUILayout.TextField(property.GetName(), property.GetValue());
                if (!string.IsNullOrEmpty(value))
                    property.SetValue(value);
                if (GUI.changed)
                    EditorUtility.SetDirty(target);
            }
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception.Message);
        }

        /*BasicPhysicsProperty property = (BasicPhysicsProperty)target;
        if (property.EditMode != BasicPhysicsProperty.EditModeEnum.Const)
        {
            string value = EditorGUILayout.TextField(property.GetName(), property.GetValue());
            if (!string.IsNullOrEmpty(value))
                property.SetValue(value);
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }
        else
        {
            EditorGUILayout.TextField(property.GetName(), property.GetValue());
        }#1#
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}
*/
