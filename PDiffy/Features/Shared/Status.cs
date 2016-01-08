namespace PDiffy.Features.Shared
{
	public class Status
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		public static Status Ok
		{
			get
			{
				return new Status
				{
					Success = true,
					Message = "Succeeded."
				};
			}
		}

		public static Status HumanComparisonRequired
		{
			get
			{
				return new Status
				{
					Success = false,
					Message = "Human comparison is required before any more difference images can be generated"
				};
			}
		}

		public static Status GetWrongInput(string name)
		{
			return new Status
			{
				Success = false,
				Message = string.Format("There has been an issue with the input. element name: {0}.", name)
			};
		}
	}

	public class Body
	{
		public string name { get; set; }
		public byte[] content { get; set; }
	}
}