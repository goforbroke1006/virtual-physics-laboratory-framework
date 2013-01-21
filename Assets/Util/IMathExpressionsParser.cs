using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IMathExpressionsParser
{
    void Process(string data, List<PhysObject> physObjects);
    void ApplyVariable(string identifier, string propertyName, object value);
}