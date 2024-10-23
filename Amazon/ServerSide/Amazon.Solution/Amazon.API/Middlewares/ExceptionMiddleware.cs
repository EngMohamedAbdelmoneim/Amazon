using System.Net;
using System.Text.Json;
using Amazon.API.Errors;

namespace Amazon.API.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IHostEnvironment environment;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
		{
			this.next = next;
			this.logger = logger;
			this.environment = environment;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next.Invoke(httpContext);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);//Development Environment

				//Response Header
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


				//Response Body
				var response = environment.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response, options);

				await httpContext.Response.WriteAsync(json);

			}

		}
	}
}
