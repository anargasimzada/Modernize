﻿using System.ComponentModel.DataAnnotations;

namespace Healet.ViewModels.Account
{
    public class LoginVM
    {
		public string UserNameOrEmail { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
