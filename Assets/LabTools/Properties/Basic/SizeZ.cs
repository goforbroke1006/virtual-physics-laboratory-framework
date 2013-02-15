using System.Globalization;
using UnityEngine;

//[AddComponentMenu("VPL Properties/Native/Size Z")]
[AddComponentMenu("Физ.свойства (основные)/Размер Z")]
public class SizeZ : BasicPhysicsProperty
{
    public override string GetName()
    {
        return "SizeZ";
    }

    public override string GetValue()
    {
        return transform.localScale.z.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.localScale = new Vector3(
            transform.rotation.x,
            transform.rotation.y,
            float.Parse(val)
            );
    }
}