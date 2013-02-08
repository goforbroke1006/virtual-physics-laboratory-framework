using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

public class MapleBuilder : IMathExpressionsBuilder
{
    public string GetSystemVariables(float start, float stop, float step, float current)
    {
        return string.Format(
            "restart: " +
            "start_time:={0}: " +
            "stop_time:={1}: " +
            "step_time:={2}: " +
            "current_time:={3}: " +
            "count_calculates:=(stop_time-start_time)/step_time: " +
            "counter:=1: ",
            start, stop, step, current
            );
    }

    public string GetDefineVariableCode(string physObjId, string propName, string propValue)
    {
        return string.Format("{0}__{1}:={2}: ", physObjId, propName, propValue);
    }

    public string GetDefineFieldVariableCode(string physObjId, string propName)
    {
        return string.Format("{0}__{1}__field(1..count_calculates): ", physObjId, propName);
    }

    public string SaveToFieldVariableCode(string physObjId, string propName)
    {
        return string.Format("{0}__{1}__field[counter]:={0}__{1}: ", physObjId, propName);
    }

    public string ReturnFieldVariableCode(string physObjId, string propName)
    {
        return string.Format("{0}__{1}__field = seq({0}__{1}__field[i], i=1..count_calculates); print('endl'); ", physObjId, propName);
    }
}
