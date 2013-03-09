using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class MapleCodeUtil
{
    // builder
    public static readonly string DefineAndSetVariableTemplate = "{0}__{1}:={2}: \n";
    public static readonly string DefineFieldVariableTemplate = "{0}__{1}__field(1..calc_count): \n";
    public static readonly string CreateFormulaTemplate = "{0} := {1}: \n";
    public static readonly Regex FormulaRegex = new Regex(@"([a-zA-Z_]+[:=|\s]{2,}[a-zA-Z0-9_\.\+\-\*\/\(\)\:\=]+[\s|:$]*)");
    public static readonly string FillFieldTemplate = "{0}__{1}__field[counter]:={0}__{1}: \n";
    public static readonly string ReturnFieldTemplate = "{0}__{1}__field = seq({0}__{1}__field[i], i=1..calc_count); \n";

    // parser 
    public static readonly Regex FieldRegex = new Regex(@"([\w]+__[\w]+)__field[\s|=|\s|\(]+([0-9e\s,.\-]+)[\)]");
    public static readonly Regex ValueRegex = new Regex(@"([-0-9.e]+)[,]*");
    public static readonly Regex VariableRegex = new Regex(@"([A-Za-z]+)__([A-Za-z]+)");
    public static readonly Regex ValueDivideRegex = new Regex(@"([-]*)([0-9]*)[.]*([0-9]*)([e]{0,1})([+-]*)([0-9]*)");

    // calculator
    public static readonly Regex CodeItemRegex = new Regex(@"(print\(\'endl\'\)\;)|([a-zA-Z_]+\s=\sseq\([\w_\[\]\(\)\,\.\=\s]+[;$])|(while[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)\=\<\>\:]+end:)|([a-zA-Z_]+:=[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)]+[:$])");

}
