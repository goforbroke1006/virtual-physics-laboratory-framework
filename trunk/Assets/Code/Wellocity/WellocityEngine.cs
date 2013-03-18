using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WellocityEngine
{
    public static string MergeTemplate(string fileName, Dictionary<string, string> context)
    {
        TextAsset textAsset = Resources.Load(fileName) as TextAsset;
        BeanManager.GetOutputConsole().AddMessage("Template merge result:\n" + textAsset.text);
        return MergeTemplate(context, textAsset.text);
    }

    public static string MergeTemplate(Dictionary<string, string> context, string data)
    {
        foreach (KeyValuePair<string, string> item in context)
            if (data.IndexOf("${" + item.Key + "}", StringComparison.Ordinal) > -1)
                data = data.Replace("${" + item.Key + "}", item.Value);

        //Debug.Log("WellocityEngine - MergeTemplate - " + data);
        return data;
    }
}