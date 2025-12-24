using Hotel_Management.DOMAIN.Exceptions;
using Hotel_Management.Shared.ErrorToReturn;

namespace Hotel_Management.Customized_Exceptions
{
    public class ManageExceptionMiddlWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ManageExceptionMiddlWare> logger;

        public ManageExceptionMiddlWare( RequestDelegate next,ILogger<ManageExceptionMiddlWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var response = new ErrorBody()
                    {
                        statuscode = StatusCodes.Status404NotFound,
                        Massege =$"{context.Request.Path} Is Not Found"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                //header

                context.Response.StatusCode = ex switch
                {
                    UserNotFoundEx => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";
                //body
                var response = new ErrorBody()
                {
                    statuscode = context.Response.StatusCode,
                    Massege=ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
