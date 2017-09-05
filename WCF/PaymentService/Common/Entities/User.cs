﻿using System.Collections.Generic;

namespace Common.Entities
{
	public class User
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public List<Account> Accounts { get; set; }
	}
}