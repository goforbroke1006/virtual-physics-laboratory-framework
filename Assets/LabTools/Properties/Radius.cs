using System;
using System.Globalization;
using UnityEngine;

[AddComponentMenu("VPL Properties/Native/Radius")]
public class Radius : PhysicsProperty
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
            case OrientationEnum.XY:
                return Convert.ToString((transform.localScale.x + transform.localScale.y) / 2);
            case OrientationEnum.XZ:
                return Convert.ToString((transform.localScale.x + transform.localScale.z) / 2);
            case OrientationEnum.YZ:
                return Convert.ToString((transform.localScale.y + transform.localScale.z) / 2);
            case OrientationEnum.XYZ:
                return Convert.ToString((transform.localScale.x + transform.localScale.y + transform.localScale.z) / 2);
        }
        return "0";
    }

    public override void SetValue(string val)
    {
        switch (CurrentOrientation)
        {
            case OrientationEnum.XY:
                transform.localScale = new Vector3(float.Parse(val), float.Parse(val), transform.rotation.z);
                break;
            case OrientationEnum.XZ:
                transform.localScale = new Vector3(float.Parse(val), transform.rotation.y, float.Parse(val));
                break;
            case OrientationEnum.YZ:
                transform.localScale = new Vector3(transform.rotation.x, float.Parse(val), float.Parse(val));
                break;
            case OrientationEnum.XYZ:
                transform.localScale = new Vector3(float.Parse(val), float.Parse(val), float.Parse(val));
                break;
        }
        
    }
}