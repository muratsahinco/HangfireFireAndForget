namespace Common.Hangfire.Jobs
{
    public class TempScheduleJobManager
    {
        public async Task Process(int TempData)
        {
            await Task.Delay(5000);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(i + TempData);
            }
        }
    }
}
