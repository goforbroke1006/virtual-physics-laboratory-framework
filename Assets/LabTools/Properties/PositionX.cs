using System;
using System.Globalization;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position X")]
public class PositionX : AbstractProperty
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
