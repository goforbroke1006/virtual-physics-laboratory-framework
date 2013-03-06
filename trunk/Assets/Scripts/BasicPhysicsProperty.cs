using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class BasicPhysicsProperty : MonoBehaviour
{
    public string CurrentValue { get; set; }
    public bool BuildField = true;

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
        if (string.IsNullOrEmpty(CurrentValue))
        {
            if (name.Equals("Weight 1kg") && GetType() == typeof(Weight)) return "1";
            if (name.Equals("Weight 100g") && GetType() == typeof(Weight)) return "0.1";
            if (name.Equals("Weight 50g") && GetType() == typeof(Weight)) return "0.05";
        }

        if (!string.IsNullOrEmpty(CurrentValue))
            return CurrentValue;
        return "";
    }

    public virtual void SetValue(string val)
    {
        CurrentValue = val;
    }
}
