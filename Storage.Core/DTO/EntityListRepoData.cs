namespace Storage.Core.Interfaces
{
    public class EntityListRepoData<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int TotalCount { get; set; }
        public int CountInList { get; set; }
    }
}
