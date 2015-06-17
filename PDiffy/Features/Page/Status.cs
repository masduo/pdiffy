namespace PDiffy.Features.Page
{
    public class Status
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static Status Ok { get { return new Status { Success = true }; } }
        public static Status HumanComparisonRequired
        {
            get
            {
                return new Status
                {
                    Message = "Human comparison is required before any more difference images can be generated"
                };
            }
        }
    }
}