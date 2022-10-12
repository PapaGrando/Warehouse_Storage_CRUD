﻿using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Models
{
    public class QuerySettings
    {
        [Range(0, int.MaxValue)]
        public int Offset { get; set; }

        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
    }

    public class QuerySettingsWithIdParameter : QuerySettings
    {
        public int IdParam { get; set; }
    }
}
