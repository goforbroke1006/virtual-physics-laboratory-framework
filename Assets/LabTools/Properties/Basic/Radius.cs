using System;
using System.Globalization;
using UnityEngine;

//[AddComponentMenu("VPL Properties/Native/Radius")]
[AddComponentMenu("Физ.свойства (дополнительные)/Радиус")]
public class Radius : BasicPhysicsProperty
{
    public enum OrientationEnum
    {
        XY, XZ, YZ, XYZ
    }

    public OrientationEnum CurrentOrientation = OrientationEnum.XZ;

    public override string GetName()
    {
        return "Radius";
    }

    public override string GetValue()
    {
        switch (CurrentOrientation)
        {
            case OrientationEnum.XY: return transform.localScale.x.ToString(CultureInfo.InvariantCulture);
            case OrientationEnum.XZ: return transform.localScale.x.ToString(CultureInfo.InvariantCulture);
            case OrientationEnum.YZ: return transform.localScale.y.ToString(CultureInfo.InvariantCulture);
            case OrientationEnum.XYZ: return transform.localScale.x.ToString(CultureInfo.InvariantCulture);
        }
        return "1";
    }

    public override void SetValue(string val)
    {
        base.SetValue(val);

        switch (CurrentOrientation)
        {
            case OrientationEnum.XY:
                transform.localScale = new Vector3(float.Parse(GetValue()), float.Parse(GetValue()), transform.localScale.z);
                break;
            case OrientationEnum.XZ:
                transform.localScale = new Vector3(float.Parse(GetValue()), transform.localScale.y, float.Parse(GetValue()));
                break;
            case OrientationEnum.YZ:
                transform.localScale = new Vector3(transform.localScale.x, float.Parse(GetValue()), float.Parse(GetValue()));
                break;
            case OrientationEnum.XYZ:
                transform.localScale = new Vector3(float.Parse(GetValue()), float.Parse(GetValue()), float.Parse(GetValue()));
                break;
        }
        
    }
}