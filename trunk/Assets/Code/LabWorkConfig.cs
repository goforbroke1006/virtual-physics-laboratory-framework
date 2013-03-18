using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LabworkConfig
{
    public float Start { get; set; }
    public float Finish { get; set; }
    public float Step { get; set; }
    public float Current { get; set; }
    public string AdditionalVars { get; set; }
    public string StopCondition { get; set; }
}