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

    private int _index;

    

    public MapleParser(List<PhysicsObject> physicsObjects) : base(physicsObjects)
    {
        _index = 0;
    }

    public override void Process(string data)
    {
        _fields = new Dictionary<string, List<string>>();

        var fieldMatches = MapleCodeUtil.FieldRegex.Matches(data);
        foreach (Match fieldMatch in fieldMatches)
        {
            List<string> values = new List<string>();
            var valuesCollection = MapleCodeUtil.ValueRegex.Matches(fieldMatch.Groups[2].Value);

            string parsingConsoleOutput = "PARSE -> \n" + fieldMatch.Groups[1].Value + ": ";

            foreach (Match valueMatch in valuesCollection)
            {
                string generalViewValue = valueMatch.Groups[1].Value;
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
                floatValue = (float) (numberView.HasExp ? floatValue * Math.Pow(10, numberView.Degr) : floatValue);
                floatValue = !numberView.IsPositive ? floatValue*(-1) : floatValue;

                values.Add(floatValue.ToString(CultureInfo.InvariantCulture));
                parsingConsoleOutput += floatValue.ToString(CultureInfo.InvariantCulture) + ",";
            }

            _fields.Add(fieldMatch.Groups[1].Value, values);

            //OutputConsole.GetInstance().AddMessage(parsingConsoleOutput);
            Debug.Log(parsingConsoleOutput);
        }
    }

    public override int Apply()
    {
        if (_fields != null && _fields.Count > 0 && _index < _fields.First().Value.Count)
        {
            foreach (KeyValuePair<string, List<string>> field in _fields)
            {
                var match = MapleCodeUtil.VariableRegex.Match(field.Key);
                ApplyVariable(match.Groups[1].Value, match.Groups[2].Value, field.Value[_index]);
                Debug.Log(string.Format("Apply {0} {1} = {2}", match.Groups[1].Value, match.Groups[2].Value, field.Value[_index]));
            }

            if (_index + 1 <= _fields.First().Value.Count)
                _index++;
            return _index - 1;
        }
        return _index;
    }

    public override void Apply(int index)
    {
        if (_fields != null && _fields.Count > 0 && index < _fields.First().Value.Count)
            foreach (KeyValuePair<string, List<string>> field in _fields)
            {
                var match = MapleCodeUtil.VariableRegex.Match(field.Key);
                ApplyVariable(match.Groups[1].Value, match.Groups[2].Value, field.Value[index]);
                Debug.Log(string.Format("Apply(int) {0} {1} = {2}", match.Groups[1].Value, match.Groups[2].Value, field.Value[index]));
            }

        _index = index;
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
