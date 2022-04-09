using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class Cell
    {
        [Key]
        public int Id { get; set; }
        public Area AreaOwner { get; set; }
        [NotMapped]
        public CellPos Pos { get; set; }
        [Required]
        public string PosDB
        {
            get => Pos.StringDB;
            set => Pos.StringDB = value;
        }
        [NotMapped]
        public Gabarits Gabarits { get; set; }
        [Required]
        public string GabaritsDB
        {
            get => Gabarits.StringDB;
            set => Gabarits.StringDB = value;
        }
        public List<CellType> CellTypes { get; set; }
        public List<Models.Product> Products { get; set; }
    }
}
