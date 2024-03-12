using System;
namespace Core.CrossCuttingConcerns.Logging;

public class LogDetailWithException : LogDetail
{

    public string Exception { get; set; }

    public LogDetailWithException()
    {
        Exception = string.Empty;
    }

    public LogDetailWithException(string fullName, string methodName, string user, List<LogParameter> parameters, string exception)
    {
        FullName = fullName;
        MethodName = methodName;
        User = user;
        Parameters = parameters;
        Exception = exception;
    }
}

