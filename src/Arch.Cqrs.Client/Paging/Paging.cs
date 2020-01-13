using System.Runtime.Serialization;

namespace Arch.Cqrs.Client.Paging
{
    public class Paging<T>
    {
        public Paging() : this(0, int.MaxValue) { }

        public Paging(int skip, int top)
        {
            SortDirection = SortDirection.Ascending;
            Skip = skip;
            Top = top;
        }

        public SortDirection SortDirection { get; set; }
        public string SortColumn { get; set; }
        public int Skip { get; set; }
        public int Top { get; set; }

        public override string ToString() => $"SortColumn: {SortColumn}, Top: {Top}, Skip: {Skip}";
    }
}
