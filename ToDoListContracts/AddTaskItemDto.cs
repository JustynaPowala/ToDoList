﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListContracts
{
	public class AddTaskItemDto
	{
		public DateTime Date { get; set; }
		public string Content { get; set; }
	}
}
