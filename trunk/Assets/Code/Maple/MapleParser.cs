using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapleParser : AbstractParser
{
    private Dictionary<string, List<string>> _fields;

    private int _index;

    private readonly Regex _fieldRegex = new Regex(@"([\w]+__[\w]+)__field[\s|=|\s|\(]+([0-9\s,.\-]+)[\)]");
    private readonly Regex _valueRegex = new Regex(@"([-0-9.]+)[|,|\s]*");
    private readonly Regex _variableRegex = new Regex(@"([A-Za-z]+)__([A-Za-z]+)");

    public MapleParser(List<PhysicsObject> physicsObjects) : base(physicsObjects)
    {
        _index = 0;
    }

    public override void Process(string data)
    {
        _fields = new Dictionary<string, List<string>>();

        var fieldsCollection = _fieldRegex.Matches(data);
        foreach (Match fieldMatch in fieldsCollection)
        {
            List<string> values = new List<string>();
            var valuesCollection = _valueRegex.Matches(fieldMatch.Groups[2].Value);
            foreach (Match valueMatch in valuesCollection)
                values.Add(valueMatch.Groups[1].Value);

            _fields.Add(fieldMatch.Groups[1].Value, values);
            
            Debug.Log(
                string.Format("MapleParser - Process - Add field \"{0} with {1} values.", 
                fieldMatch.Groups[1].Value, 
                values.Count));
        }
    }

    public override void Apply()
    {
        //Debug.Log("MapleParser - Apply() - Start...");
        if (_fields != null && _fields.Count > 0 && _index < _fields.First().Value.Count)
        {
            foreach (KeyValuePair<string, List<string>> field in _fields)
            {
                var match = _variableRegex.Match(field.Key);
                ApplyVariable(match.Groups[1].Value, match.Groups[2].Value, field.Value[_index]);
                Debug.Log(string.Format("Apply {0} {1} = {2}", match.Groups[1].Value, match.Groups[2].Value, field.Value[_index]));
            }

            if (_index + 1 <= _fields.First().Value.Count)
                _index++;
        }
    }

    public override void Apply(int index)
    {
        if (_fields != null && _fields.Count > 0 && index < _fields.First().Value.Count)
            foreach (KeyValuePair<string, List<string>> field in _fields)
            {
                var match = _variableRegex.Match(field.Key);
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
                    AbstractProperty property = physObject.GetProperty(propertyName);
                    if (property != null)
                        property.SetValue(propertyValue);
                }
    }
}
