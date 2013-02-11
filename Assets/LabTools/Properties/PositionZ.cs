using System.Globalization;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position Z")]
public class PositionZ : AbstractProperty
{
    public override string GetName()
    {
        return "PosZ";
    }

    public override string GetValue()
    {
        return transform.position.z.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            float.Parse(val)
            );
    }
}
