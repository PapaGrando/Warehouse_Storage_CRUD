using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
        public List<SubArea> SubAreas { get; set; }
        public List<Cell> Cells { get; set; }
    }
}
