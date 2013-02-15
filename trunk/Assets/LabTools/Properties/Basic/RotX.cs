using System.Globalization;
using UnityEngine;

//[AddComponentMenu("VPL Properties/Native/Rotation X")]
[AddComponentMenu("Физ.свойства (основные)/Поворот X")]
public class RotX : BasicPhysicsProperty
{
    public override string GetName()
    {
        return "RotX";
    }

    public override string GetValue()
    {
        return transform.rotation.x.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.rotation = new Quaternion(
            float.Parse(val),
            transform.rotation.y,
            transform.rotation.z,
            transform.rotation.w
            );
    }
}