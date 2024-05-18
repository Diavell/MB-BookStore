namespace MB.Web.Models
{
    public class AdminReturnResult
    {
        public class TimePeriod
        {
            public string Annual { get; set; }
            public string Monthly { get; set; }
        }

        public enum TimePeriodEnum
        {
            Monthly,
            Annual
        }
    }
}
