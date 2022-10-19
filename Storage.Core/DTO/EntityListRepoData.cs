using Storage.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Interfaces
{
    public class EntityListRepoData<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int TotalCount { get; set; }
        public int CountInList { get; set; }
    }
}
