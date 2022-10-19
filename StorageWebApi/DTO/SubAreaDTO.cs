using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Interfaces
{
    public class SubAreaDTOReadOnlyInfo
    {
        public int Id { get; set; }

        //Кол-во ячеек в высоту, длину, и ширину (В ширину макс 2, тк будет невозможно брать товар в центре)
        [Range(1, int.MaxValue)]
        public int HeightCells { get; init; }
        [Range(1, int.MaxValue)]
        public int LengthCells { get; init; }
        [Range(1, 2)]
        public int WidthCells { get; init; }
        public int AreaId { get; init; }
        public int CellTypeId { get; init; }
        public CellTypeDTO CellType { get; init; }
        public IEnumerable<CellDTOInfoReadOnly> Cells { get; init; }
    }

    public class SubAreaDTOShortInfo
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int HeightCells { get; init; }
        [Range(1, int.MaxValue)]
        public int LengthCells { get; init; }
        [Range(1, 2)]
        public int WidthCells { get; init; }
        public int AreaId { get; init; }
        public int CellTypeId { get; init; }
        public CellTypeDTO CellType { get; init; }
    }
}
