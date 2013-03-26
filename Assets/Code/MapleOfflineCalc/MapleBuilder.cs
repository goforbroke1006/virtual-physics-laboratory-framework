using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapleBuilder : AbstractBuilder
{
    public override string GetCode_Labwork(LabworkConfig config, List<PhysicsObject> physicsObjects)
    {
        Dictionary<string, string> context = new Dictionary<string, string>();

        context.Add("start",        config.Start.ToString(CultureInfo.InvariantCulture));
        context.Add("stop",         config.Finish.ToString(CultureInfo.InvariantCulture));
        context.Add("step",         config.Step.ToString(CultureInfo.InvariantCulture));
        context.Add("ctime",        config.Current.ToString(CultureInfo.InvariantCulture));

        context.Add("additional_vars", config.AdditionalVars);
        context.Add("stop_condition", config.StopCondition);

        context.Add("define_variables", 
            GetCode_DefineVariableCode(physicsObjects));
        context.Add("define_variables_fields", 
            GetCode_DefineFieldVariableCode(physicsObjects));
        context.Add("paste_formulas", 
            GetCode_PastedFormulasCode(physicsObjects));
        context.Add("fill_fields", 
            GetCode_FillFieldWithVariableCode(physicsObjects));
        context.Add("return_fields", 
            GetCode_ReturnFieldVariableCode(physicsObjects));

        string result = WellocityEngine.MergeTemplate("Codes/template_2", context);

        BeanManager.GetOutputConsole().AddMessage("Builder result: " + result);
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

    public override string GetCode_DefineVariableCode(List<PhysicsObject> physicsObjects)
    {
        string result = "";
        foreach (PhysicsObject physicsObject in physicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                result += string.Format(MapleCodeUtil.DefineAndSetVariableTemplate, physicsObject.Identifier, property.GetName(), property.GetValue());
        return result;
    }

    public override string GetCode_DefineFieldVariableCode(List<PhysicsObject> physicsObjects)
    {
        string result = "";
        foreach (PhysicsObject physicsObject in physicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.DefineFieldVariableTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetCode_PastedFormulasCode(List<PhysicsObject> physicsObjects)
    {
        string result = "";
        foreach (PhysicsObject po in physicsObjects)
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

    public override string GetCode_FillFieldWithVariableCode(List<PhysicsObject> physicsObjects)
    {
        string result = "";
        foreach (PhysicsObject physicsObject in physicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.FillFieldTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetCode_ReturnFieldVariableCode(List<PhysicsObject> physicsObjects)
    {
        string result = "";
        foreach (PhysicsObject physicsObject in physicsObjects)
            foreach (BasicPhysicsProperty property in physicsObject.GetProperties())
                if (property.BuildField)
                    result += string.Format(MapleCodeUtil.ReturnFieldTemplate, physicsObject.Identifier, property.GetName());
        return result;
    }
}
