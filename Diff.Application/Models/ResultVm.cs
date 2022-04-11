using System.Collections.Generic;

namespace Diff.Application.Models
{
    public class Differences
    {
        public int Offset { get; set; }
        public int Diff { get; set; }
    }

    public class ResultVm
    {
        public int Id { get; set; }
        public string ResultMessage { get; set; }
        public IList<Differences> Differences { get; set; } = new List<Differences>();
    }
}