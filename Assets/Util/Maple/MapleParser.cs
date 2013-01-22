using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class MapleParser : IMathExpressionsParser
{
    private List<PhysObject> _physObjects;
    private readonly Regex _variableRegex = new Regex(@"[\w_]+__[\w_]+(\s:=|:=|\s:=\s|:=\s)[0-9.][\n]");

    public void Process(string data, List<PhysObject> physObjects)
    {
        _physObjects = physObjects;

        var matches = _variableRegex.Matches(data);
        for (int i = 0; i < matches.Count; i++)
            ApplyVariable(
                matches[i].Groups[1].Value, 
                matches[i].Groups[2].Value, 
                matches[i].Groups[3].Value
                );
    }

    public void ApplyVariable(string identifier, string propertyName, object propertyValue)
    {
        foreach (PhysObject physObject in _physObjects)
        {
            if (physObject.Identifier == identifier)
            {
                IProperty property = physObject.GetProperty(propertyName);
                if (property != null)
                    property.SetValue(propertyValue);
            }
        }
    }
}
