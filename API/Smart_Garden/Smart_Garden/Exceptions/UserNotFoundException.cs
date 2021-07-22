using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserNotFoundException : Exception
{
    public string _message;
    public override string Message { get => _message; }
    public UserNotFoundException(object username,object password)
    {
        _message = "Could not find user: '" + username.ToString() + "' with password: '" + password.ToString() + "'";
    }
    public UserNotFoundException(object userId)
    {
        _message = "Could not find user " + userId.ToString();
    }
}
