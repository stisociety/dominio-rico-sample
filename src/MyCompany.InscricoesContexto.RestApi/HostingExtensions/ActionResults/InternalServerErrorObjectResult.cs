namespace Microsoft.ActionResults
{
    using AspNetCore.Http;
    using AspNetCore.Mvc;

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}