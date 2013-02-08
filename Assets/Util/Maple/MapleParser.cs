using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

class MapleParser : IMathExpressionsParser
{
    private List<PhysObject> _physObjects;
    private readonly Regex _variableRegex = new Regex(@"([A-Za-z]+)__([A-Za-z]+) := ([0-9.-]+)");

    public void Process(string data, List<PhysObject> physObjects)
    {
        /*while (data.IndexOf(" ") > -1)
        {
            data.Replace(" ", "");
        }*/

        if (data.IndexOf(".") == 0 || (data.IndexOf(".") == 1 && data.IndexOf("-") == 0))
            data = data.Replace(".", "0.");

        Debug.Log("Applying to " + data);
        _physObjects = physObjects;

        var matches = _variableRegex.Matches(data);

        Debug.Log("matches.Count = " + matches.Count);

        for (int i = 0; i < matches.Count; i++)
        {
            Debug.Log("Apply: " + matches[i].Groups[2].Value);

            string numberVal = matches[i].Groups[3].Value;
            if (numberVal.Substring(0, 1) == ".")
                numberVal.Insert(0, "0");

            ApplyVariable(
                matches[i].Groups[1].Value,
                matches[i].Groups[2].Value,
                numberVal
                );
        }
    }

    public void ApplyVariable(string identifier, string propertyName, string propertyValue)
    {
        foreach (PhysObject physObject in _physObjects)
        {
            if (physObject.Identifier == identifier)
            {
                AbstractProperty property = physObject.GetProperty(propertyName);
                if (property != null)
                    property.SetValue(propertyValue);
            }
        }
    }
}
