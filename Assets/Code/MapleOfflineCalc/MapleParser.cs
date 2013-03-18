using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapleParser : AbstractParser
{
    private Dictionary<string, List<string>> _fields;

    public int Index { get; private set; }

    public void ClearFields()
    {
        _fields = null;
    }

    public bool HasFields()
    {
        return (_fields != null && _fields.Count > 0);
    }

    public int FieldsSize()
    {
        if (_fields != null && _fields.Count > 0 && _fields.First().Value.Count > 0)
            return _fields.First().Value.Count;
        return 0;
    }

    public MapleParser(List<PhysicsObject> physicsObjects)
        : base(physicsObjects)
    {
        Index = 0;
    }

    public override void Process(string data)
    {
        _fields = new Dictionary<string, List<string>>();

        var fieldMatches = MapleCodeUtil.FieldRegex.Matches(data);

        //BeanManager.GetOutputConsole().AddMessage("Maple parser - Match found " + fieldMatches.Count);

        foreach (Match fieldMatch in fieldMatches)
        {
            List<string> values = new List<string>();
            var valuesCollection = MapleCodeUtil.FloatValueRegex.Matches(fieldMatch.Groups[2].Value);

            string parsingConsoleOutput = "PARSE -> \n" + fieldMatch.Groups[1].Value + ": ";

            foreach (Match valueMatch in valuesCollection)
            {
                string generalViewValue = valueMatch.Groups[2].Value;
                Match match = MapleCodeUtil.ValueDivideRegex.Match(generalViewValue);

                NumberView numberView = new NumberView
                {
                    IsPositive = (match.Groups[1].Value != "-"),
                    NumberBeforePoint = Convert.ToInt64(match.Groups[2].Value.Length > 0 ? match.Groups[2].Value : "0"),
                    NumberAfterPoint = Convert.ToSingle(match.Groups[3].Value.Length > 0 ? "0." + match.Groups[3].Value : "0"),
                    HasExp = (match.Groups[4].Value == "e"),
                    Degr = Convert.ToInt32(match.Groups[6].Value.Length > 0 ? match.Groups[6].Value : "0") * (match.Groups[5].Value == "-" ? -1 : 1)
                };

                float floatValue = 0;
                floatValue += numberView.NumberBeforePoint;
                floatValue += numberView.NumberAfterPoint;
                floatValue = (float)(numberView.HasExp ? floatValue * Math.Pow(10, numberView.Degr) : floatValue);
                floatValue = !numberView.IsPositive ? floatValue * (-1) : floatValue;

                values.Add(floatValue.ToString(CultureInfo.InvariantCulture));
                parsingConsoleOutput += floatValue.ToString(CultureInfo.InvariantCulture) + ",";
            }

            _fields.Add(fieldMatch.Groups[1].Value, values);

            BeanManager.GetOutputConsole().AddMessage(parsingConsoleOutput);
        }
    }

    public override int Apply()
    {
        Apply(Index);

        if (_fields != null && _fields.Count > 0 && Index < _fields.First().Value.Count)
        {
            if (Index + 1 <= _fields.First().Value.Count)
                Index++;
            return Index - 1;
        }
        return Index;
    }

    public override void Apply(int index)
    {
        if (_fields != null && _fields.Count > 0 && index < _fields.First().Value.Count)
            foreach (KeyValuePair<string, List<string>> field in _fields)
            {
                var match = MapleCodeUtil.VariableRegex.Match(field.Key);
                ApplyVariable(match.Groups[1].Value, match.Groups[2].Value, field.Value[index]);
            }

        Index = index;
        //BeanManager.GetOutputConsole().AddMessage("Apply [" + Index + "] fields value.");
    }

    public override void ApplyVariable(string identifier, string propertyName, string propertyValue)
    {
        if (PhysicsObjectsManager.GetPhysicsObjects() != null)
            foreach (PhysicsObject physObject in PhysicsObjectsManager.GetPhysicsObjects())
                if (physObject.Identifier == identifier)
                {
                    BasicPhysicsProperty property = physObject.GetProperty(propertyName);
                    if (property != null)
                        property.SetValue(propertyValue);
                }
    }
}
