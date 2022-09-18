using Storage.Core.DTO;
using Storage.Core.Models.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Factory
{
    public class SubAreaFactory : ISubAreaFactory
    {
        /// <exception cref="ArgumentException"/>
        public SubArea Create(SubAreaConfiguration config)
        {
            if (config is null)
                return null;

            var subArea = new SubArea()
            {
                LengthCells = config.Length,
                WidthCells = config.Width,
                HeightCells = config.Height
            };
            subArea.Cells = FullByCellsUsingConfig(config);

            return subArea;
        }

        private List<Cell> FullByCellsUsingConfig(SubAreaConfiguration config)
        {
            var outList = new List<Cell>
                (config.Length * config.Width * config.Height);

            for (int x = 1; x <= config.Length; x++)
                for (int y = 1; y <= config.Height; y++)
                    for (int z = 1; z <= config.Width; z++)
                    {
                        var c = new Cell(x, y, z)
                        {
                            CellTypeId = config.CellType.Id
                        };

                        outList.Add(c);
                    }

            return outList;
        }

    }
}
