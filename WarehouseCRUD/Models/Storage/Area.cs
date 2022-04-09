using System.Collections.Generic;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cell> Cells { get; set; }
    }
}
