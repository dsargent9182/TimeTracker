namespace JobManager.Configuration
{
	public interface IJobManagerConfiguration
	{
		string HangfireConnectionString { get; set; }
		int JobRetentionInDays { get; }
	}
}
