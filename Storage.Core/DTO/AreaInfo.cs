using System.ComponentModel.DataAnnotations;

namespace Storage.Core.DTO
{
    public class AreaInfo
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; }
        [Display(Name = "Кол-во зон")]
        public int SubAreasCount { get; }
        [Display(Name = "Кол-во ячеек")]
        public int CellsCount { get; }

        public AreaInfo(int id, string name, int subAreasCount, int cellsCount)
        {
            Name = name;
            SubAreasCount = subAreasCount;
            CellsCount = cellsCount;
            Id = id;
        }
    }
}
