using System.Globalization;
using UnityEngine;

[AddComponentMenu("VPL Properties/Native/Rotation Z")]
public class RotationZ : AbstractProperty
{
    public override string GetName()
    {
        return "RotatZ";
    }

    public override string GetValue()
    {
        return transform.rotation.z.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.rotation = new Quaternion(
            transform.rotation.x,
            transform.rotation.y,
            float.Parse(val),
            transform.rotation.w
            );
    }
}