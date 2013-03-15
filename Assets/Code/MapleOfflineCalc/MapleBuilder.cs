using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapleBuilder : AbstractBuilder
{
    public MapleBuilder(List<PhysicsObject> physicsObjects) : base(physicsObjects)
    {
    }

    public override string GetCode_Labwork(LabworkConfig config)
    {
        Dictionary<string, string> context = new Dictionary<string, string>();

        context.Add("start",        config.Start.ToString(CultureInfo.InvariantCulture));
        context.Add("stop",         config.Finish.ToString(CultureInfo.InvariantCulture));
        context.Add("step",         config.Step.ToString(CultureInfo.InvariantCulture));
        context.Add("ctime",        config.Current.ToString(CultureInfo.InvariantCulture));

        context.Add("additional_vars", config.AdditionalVars);

        context.Add("define_variables",         GetCode_DefineVariableCode());
        context.Add("define_variables_fields",  GetCode_DefineFieldVariableCode());
        context.Add("paste_formulas",           GetCode_PastedFormulasCode());
        context.Add("fill_fields",              GetCode_FillFieldWithVariableCode());
        context.Add("return_fields",            GetCode_ReturnFieldVariableCode());

        string result = WellocityEngine.MergeTemplate("Codes/template_1", context);

        Debug.Log(result);
        OutputConsole.GetInstance().AddMessage("Builder result: " + result);
        return result;
    }

    public override string GetCode_DefineAdditionalVarsCode(string additionalVars)
    {
        string result = "";
        var collection = MapleCodeUtil.AdditionalVarRegex.Matches(additionalVars);
        foreach (Match match in collection)
        {
            result += match.Groups[1].Value + ": \n";
        }
        return result;
    }

    public override string GetCode_DefineVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                result += string.Format(MapleCodeUtil.DefineAndSetVariableTemplate, physicsObject.Identifier, property.GetName(), property.GetValue());
        return result;
    }

    public override string GetCode_DefineFieldVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.DefineFieldVariableTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetCode_PastedFormulasCode()
    {
        string result = "";
        foreach (PhysicsObject po in PhysicsObjects)
        {
            foreach (BasicPhysicsProperty prop in po.GetProperties())
            {
                if (prop.BuildField && !string.IsNullOrEmpty(prop.Formula))
                {
                    string formula = string.Format(
                        MapleCodeUtil.DefineAndSetVariableTemplate, 
                        po.Identifier, 
                        prop.GetName(), 
                        prop.Formula);
                    result += formula;
                    result += "\n";
                }
            }
        }
        return result;
    }

    public override string GetCode_FillFieldWithVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.FillFieldTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetCode_ReturnFieldVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.ReturnFieldTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }
}
