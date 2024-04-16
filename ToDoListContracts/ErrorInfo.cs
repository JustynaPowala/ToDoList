using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListContracts
{
	public class ErrorInfo
	{
		public ErrorInfo(string message)
		{
			Message = message;
		}


		public string Message { get; set; }
	}
}
