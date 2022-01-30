namespace TimeTracker.Configuration
{
	public interface ITimeTrackerConfiguration
	{
		string ConnectionString{ get; set; }
		string HangfireConnectionString { get; set; }
		
		string HangfireSmtpFromEmail { get; }
		string HangfireSmtpToEmail { get; }
		string HangfireSmtpHost { get; }
		int HangfireSmtpPort { get; }
		string HangfireSmtpUsername { get; }
		string HangfireSmtpPassword { get; }
	}
}
