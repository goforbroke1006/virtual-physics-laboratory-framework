using System.Globalization;
using UnityEngine;

[AddComponentMenu("VPL Properties/Native/Scale Z")]
public class SizeY : PhysicsProperty
{
    public override string GetName()
    {
        return "SizeY";
    }

    public override string GetValue()
    {
        return transform.localScale.y.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.localScale = new Vector3(
            transform.rotation.x,
            float.Parse(val),
            transform.rotation.z
            );
    }
}