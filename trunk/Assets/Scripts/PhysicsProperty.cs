using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class PhysicsProperty : MonoBehaviour
{
    public string CurrentValue { get; set; }

    public enum EditModeEnum
    {
        Const,
        MouseMode,
        SetValueMode
    }
    public EditModeEnum EditMode = EditModeEnum.Const;

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
