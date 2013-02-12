using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapleBuilder : AbstractBuilder
{
    public MapleBuilder(List<PhysicsObject> physicsObjects, List<Formula> formulas) : base(physicsObjects, formulas)
    {
    }

    public override string GetLabworkCode(LabworkConfig config)
    {
        Dictionary<string, string> context = new Dictionary<string, string>();
        context.Add("start",        config.Start.ToString(CultureInfo.InvariantCulture));
        context.Add("stop",         config.Finish.ToString(CultureInfo.InvariantCulture));
        context.Add("step",         config.Step.ToString(CultureInfo.InvariantCulture));
        context.Add("ctime",        config.Current.ToString(CultureInfo.InvariantCulture));
        context.Add("define_variables",         GetDefineVariableCode());
        context.Add("define_variables_fields",  GetDefineFieldVariableCode());
        context.Add("paste_formulas",           GetPastedFormulasCode());
        context.Add("fill_fields",              GetFillFieldWithVariableCode());
        context.Add("return_fields",            GetReturnFieldVariableCode());

        string result = WellocityEngine.MergeTemplate("Codes/template_1", context);

        Debug.Log(result);
        return result;
    }

    public override string GetDefineVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (AbstractProperty property in physicsObject.GetProperties())
                result += string.Format("{0}__{1}:={2}: \n", physicsObject.Identifier, property.GetName(), property.GetValue());
        return result;
    }

    public override string GetDefineFieldVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (AbstractProperty property in physicsObject.GetProperties())
                result += string.Format("{0}__{1}__field(1..calc_count): \n", physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetPastedFormulasCode()
    {
        string result = "";
        Regex formulaRegex = new Regex(@"([a-zA-Z_]+[:=|\s]{2,}[a-zA-Z0-9_\.\+\-\*\/\(\)\:\=]+[\s|:$]*)");
        foreach (Formula formula in Formulas)
        {
            MatchCollection collection = formulaRegex.Matches(formula.Code);
            foreach (Match match in collection)
            {
                string temp = match.Groups[1].Value;
                if (temp.Substring(temp.Length - 1, 1) != ":") temp += ":";
                result += temp + "\n";
            }
        }

        return result;
    }

    public override string GetFillFieldWithVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (AbstractProperty property in physicsObject.GetProperties())
                result += string.Format("{0}__{1}__field[counter]:={0}__{1}: \n", physicsObject.Identifier, property.GetName());
        return result;
    }

    public override string GetReturnFieldVariableCode()
    {
        string result = "";
        foreach (PhysicsObject physicsObject in PhysicsObjects)
            foreach (AbstractProperty property in physicsObject.GetProperties())
                result += string.Format("{0}__{1}__field = seq({0}__{1}__field[i], i=1..calc_count); \n", physicsObject.Identifier, property.GetName());
        return result;
    }
}
