// ReSharper disable RedundantUsingDirective
using System;
// ReSharper restore RedundantUsingDirective

namespace GGGC.Admin.Sign.Models
{
	public class User
	{
		public string UserName { get; set; }
		public int UserID { get; set; }
		public string Password { get; set; }
		public string EMailAddress { get; set; }
        public string StoreID { get; set; }
        public string Name { get; set; }

		public string PerfilID { get; set; }
		public string ImageSourcePath { get; set; }
		
	}
}
