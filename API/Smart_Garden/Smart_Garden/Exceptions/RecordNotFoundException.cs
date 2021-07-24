using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class RecordNotFoundException : Exception
{
    public string _message;
    public override string Message { get => _message; }
    public RecordNotFoundException(object id)
    {
        _message = "Could not find " + id.ToString();
    }
    public RecordNotFoundException()
    {
        _message = "Record Not Found";
    }
}

