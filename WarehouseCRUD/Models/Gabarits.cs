namespace WarehouseCRUD.Storage.Models
{
    public class Gabarits
    {
        private const char _db_separator = ';';

        public float Length { get; set; } = 1;
        public float Width { get; set; } = 1;
        public float Height { get; set; } = 1;
        public string StringDB
        {
            get => string.Join(_db_separator, Length, Width, Height);
            set
            {
                var DbArr = value.Split(_db_separator);

                Length = float.TryParse(DbArr[0], out float l) ? l : Length;
                Width = float.TryParse(DbArr[1], out float w) ? w : Width;
                Height = float.TryParse(DbArr[2], out float h) ? h : Height;
            }
        }
    }
}
