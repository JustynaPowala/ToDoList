using System.Net.Http.Json;
using ToDoList.WebUi.ApiClients;
using ToDoListContracts;

namespace ToDoList.WebUi
{
	public class ErrorHandlingHttpMessageHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfo>();

				throw new ToDoListApiException()
				{
					Message = errorInfo.Message
				};
			}
			return response;
		}
	}
}
