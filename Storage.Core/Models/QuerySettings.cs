using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Models
{
    public class QuerySettings
    {
        [Range(1, int.MaxValue)]
        public int PageNo { get; set; }

        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
    }

    public class QuerySettingsWithIdSubArea : QuerySettings
    {
        public int IdSubArea { get; set; }
    }
}
