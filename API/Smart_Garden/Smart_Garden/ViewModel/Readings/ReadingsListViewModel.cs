using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ReadingsListViewModel : JsonViewModel
{
    public Garden Garden { get; set; }
    public List<Reading> Readings { get; set; }
}
