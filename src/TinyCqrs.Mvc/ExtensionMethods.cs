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

    private static ObjectResult ObjectResultWithBody<TOutput>(int statusCode, ICmdResult<TOutput> cmdResult)
    {
        if (statusCode >= 200 && statusCode <= 299)
            return new ObjectResult(cmdResult.Output) { StatusCode = statusCode };

        return new ObjectResult(string.Join(", ", cmdResult.Issues.Select(x => x.Message))) { StatusCode = statusCode };
    }

    public static IActionResult ToActionResult<TOutput>(this ICmdResult<TOutput> cmdResult)
    {
        var statusCode = StatusCodeFromCmdResult(cmdResult);
        var statusCodeInt = (int) statusCode;

        return cmdResult.Output == null 
            ? new StatusCodeResult(statusCodeInt) 
            : ObjectResultWithBody<TOutput>(statusCodeInt, cmdResult);
    }

    public static async Task<IActionResult> ToActionResult<TOutput>(this Task<ICmdResult<TOutput>> pendingCmdResult)
    {
        var cmdResult = await pendingCmdResult;
        return cmdResult.ToActionResult();
    }
}