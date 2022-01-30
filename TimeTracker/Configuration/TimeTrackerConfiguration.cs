namespace TimeTracker.Configuration
{
	public class TimeTrackerConfiguration : ITimeTrackerConfiguration
	{
		public string ConnectionString { get; set; }
		public string HangfireConnectionString { get; set; }

		public string HangfireSmtpFromEmail { get; set; }

		public string HangfireSmtpToEmail { get; set; }

		public string HangfireSmtpHost { get; set; }

		public int HangfireSmtpPort { get; set; }

		public string HangfireSmtpUsername { get; set; }

		public string HangfireSmtpPassword { get; set; }
	}
}
