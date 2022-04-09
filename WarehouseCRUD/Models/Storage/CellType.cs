namespace WarehouseCRUD.Storage.Models.Storage
{
    /// <summary>
    /// Тип ячейки, обозначающая, какой тип товаров можно хранить
    /// Мелкий, Крупногабарит, Негабарит, Еда, Хрупкое
    /// </summary>
    public class CellType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
