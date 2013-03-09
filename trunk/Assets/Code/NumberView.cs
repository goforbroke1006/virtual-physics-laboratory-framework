using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NumberView
{
    public bool IsPositive { get; set; }
    public long NumberBeforePoint { get; set; }
    public float NumberAfterPoint { get; set; }
    public bool HasExp { get; set; }
    public int Degr { get; set; }

    public override string ToString()
    {
        return string.Format("IsPositive: {0}; Bef: {1}; Aft: {2}; HasExp: {3}; Deg: {4};", 
            IsPositive ? "true" : "false",
            NumberBeforePoint, 
            NumberAfterPoint,
            HasExp ? "true" : "false", 
            Degr);
    }
}
