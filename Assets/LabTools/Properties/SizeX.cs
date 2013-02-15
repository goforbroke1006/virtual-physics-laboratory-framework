using System.Globalization;
using UnityEngine;

//[AddComponentMenu("VPL Properties/Size X")]
[AddComponentMenu("Физ.свойства (основные)/Размер X")]
public class SizeX : PhysicsProperty
{
    public override string GetName()
    {
        return "SizeX";
    }

    public override string GetValue()
    {
        return transform.localScale.x.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.localScale = new Vector3(
            float.Parse(val),
            transform.rotation.y,
            transform.rotation.z
            );
    }
}