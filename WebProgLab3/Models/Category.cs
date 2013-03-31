using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgLab3.Models
{

	public class Category
	{

		private int id { get; private set; }
		private string name { get; private set; }

		public Category(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

	}

}