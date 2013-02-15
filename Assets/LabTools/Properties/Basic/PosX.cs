using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using System.Collections;

//[AddComponentMenu("VPL Properties/Position X")]
[AddComponentMenu("Физ.свойства (основные)/Координаты X")]
public class PosX : BasicPhysicsProperty
{
    public override string GetName()
    {
        return "PosX";
    }

    public override string GetValue()
    {
        return transform.position.x.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.position = new Vector3(
            float.Parse(val),
            transform.position.y,
            transform.position.z
            );
    }
}
