using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Values
{
    public decimal Minimum { get; set; }
    public decimal Maximum { get; set; }

    public Values(decimal min, decimal max)
    {
        Minimum = min;
        Maximum = max;
    }
}
