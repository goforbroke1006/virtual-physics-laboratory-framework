using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IMathExpressionsBuilder
{
    string GetSystemVariables(float start, float stop, float step, float current);
    string GetDefineVariableCode(string physObjId, string propName, string propValue);
    string GetDefineFieldVariableCode(string physObjId, string propName);
    string SaveToFieldVariableCode(string physObjId, string propName);
    string ReturnFieldVariableCode(string physObjId, string propName);
}