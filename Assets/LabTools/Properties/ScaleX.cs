using System.Globalization;
using UnityEngine;

[AddComponentMenu("VPL Properties/Native/Scale Z")]
public class ScaleX : AbstractProperty
{
    public override string GetName()
    {
        return "ScaleX";
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