using Microsoft.AspNetCore.Mvc;
using System.Net;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Mvc;

public static class ExtensionMethods
{
    private static HttpStatusCode StatusCodeFromCmdResult<TOutput>(ICmdResult<TOutput> cmdResult)
    {
        var parsed = Enum.TryParse<HttpStatusCode>(cmdResult.Type, out var statusCode);
        if (parsed)
            return statusCode;

        return cmdResult.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
    }

    public static IActionResult ToActionResult<TOutput>(this ICmdResult<TOutput> cmdResult)
    {
        var statusCode = StatusCodeFromCmdResult(cmdResult);
        var statusCodeInt = Convert.ToInt32(statusCode);

        return cmdResult.Output == null 
            ? new StatusCodeResult(statusCodeInt) 
            : new ObjectResult(cmdResult.Output) { StatusCode = statusCodeInt };
    }
}