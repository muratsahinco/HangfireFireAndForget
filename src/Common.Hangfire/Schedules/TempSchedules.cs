using Common.Hangfire.Jobs;
using Hangfire;

namespace Common.Hangfire.Schedules
{
    public static class TempSchedules
    {
        public static void AddTemp(int tempParam)
        {
            BackgroundJob.Enqueue<TempScheduleJobManager>(job => job.Process(tempParam));
        }
    }
}
