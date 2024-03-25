namespace Final_project.Stores
{
    public class GeneratedReportStore
    {
        public event Action<string> ReportAdded;

        public void AddPerson(string name)
        {
            ReportAdded?.Invoke(name);
        }

    }
}
