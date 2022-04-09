namespace WarehouseCRUD.Storage.Models.Storage
{
    public class CellPos
    {
        private const char _db_separator = ';';

        public int Row { get; set; } = 1;
        public int Floor { get; set; } = 1;

        public string StringDB
        {
            get => string.Join(_db_separator, Row, Floor);
            set
            {
                var DbArr = value.Split(_db_separator);

                Row = int.TryParse(DbArr[0], out int r) ? r : Row;
                Floor = int.TryParse(DbArr[1], out int f) ? f : Floor;
            }
        }
    }
}
