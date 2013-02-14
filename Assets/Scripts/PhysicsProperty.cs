using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

public class PhysicsProperty : MonoBehaviour
{
    public string CurrentValue;

    public enum EditModeEnum
    {
        Static,
        MouseMode,
        SetValueMode
    }
    public EditModeEnum EditMode = EditModeEnum.MouseMode;

    public virtual string GetName()
    {
        return "Property";
    }

    public virtual string GetValue()
    {
        if (!string.IsNullOrEmpty(CurrentValue))
            return CurrentValue;
        return "";
    }

    public virtual void SetValue(string val)
    {
        CurrentValue = val;
    }
}
