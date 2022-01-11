using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace JobManager
{
	public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize([NotNull] DashboardContext context)
		{
			//throw new NotImplementedException();
			return true;
		}
	}
}
