using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class InsertException : Exception
{
    public string _message;
    public override string Message { get => _message; }
    public InsertException(Exception exception)
    {
        if (exception.Message.Contains("Violation of UNIQUE KEY constraint"))
        {
            _message = "Violation of UNIQUE KEY constraint";
        }
        else if (exception.Message.Contains("Truncated value"))
        {
            _message = "Truncated value";
        }
    }
}
