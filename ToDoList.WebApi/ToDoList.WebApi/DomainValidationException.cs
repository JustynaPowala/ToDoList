﻿namespace ToDoList.WebApi
{
	public class DomainValidationException : Exception
	{
		public DomainValidationException(string message) :base(message)
		{
		}
	}
}
