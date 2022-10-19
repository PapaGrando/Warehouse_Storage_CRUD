namespace Storage.Core.Interfaces
{
    public class CellDTOInfoReadOnly : IBaseDTO
    {
        public int Id { get; set; }
        public string Name { get => $"{SubAreaId}||{SubAreaLengthX}-{SubAreaHeigthZ}-{SubAreaWidthY}"; }
        public int SubAreaId { get; init; }
        //Расположение ячейки в SubArea
        public int SubAreaLengthX { get; init; }
        public int SubAreaWidthY { get; init; }
        public int SubAreaHeigthZ { get; init; }

        public IEnumerable<StorageItemDTO> Items { get; init; }
    }

    public class CellDTOShortInfoReadOnly : IBaseDTO
    {
        public int Id { get; set; }
        public string Name { get => $"{SubAreaId}||{SubAreaLengthX}-{SubAreaHeigthZ}-{SubAreaWidthY}"; }
        public int SubAreaId { get; init; }
        //Расположение ячейки в SubArea
        public int SubAreaLengthX { get; init; }
        public int SubAreaWidthY { get; init; }
        public int SubAreaHeigthZ { get; init; }
    }

    public class CellDTODetailedReadOnly : IBaseDTO
    {
        public int Id { get; set; }
        public string Name { get => $"{SubAreaId}||{SubAreaLengthX}-{SubAreaHeigthZ}-{SubAreaWidthY}"; }
        public int SubAreaId { get; init; }
        public SubAreaDTOShortInfo SubArea { get; init; }
        //Расположение ячейки в SubArea
        public int SubAreaLengthX { get; init; }
        public int SubAreaWidthY { get; init; }
        public int SubAreaHeigthZ { get; init; }

        public IEnumerable<StorageItemCellDetailedInfoReadOnly> Items { get; init; }
    }
}
