using Storage.Core.DTO;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;

namespace Storage.Core.Builders
{
    public class AreaBuilder : IAreaBuilder
    {
        private Area _areaState { get; set; }

        public AreaBuilder()
        {
            _areaState = new Area();
            _areaState.SubAreas = new List<SubArea>();
        }

        public IAreaBuilder AddSubArea(SubAreaConfiguration conf, int count = 1)
        {
            for (int i = 0; i < count; i++)
                _areaState.SubAreas.Add(BuildSubAreaWithCells(conf));

            return this;
        }

        public IAreaBuilder ApplyConfig(AreaConfiguration config)
        {
            _areaState.Id = config.Id;
            _areaState.Name = config.Name;

            _areaState.SubAreas.Clear();

            foreach (var sa in config.SubAreasToCreate)
                AddSubArea(sa);

            return this;
        }

        public Area Build() => _areaState;

        private SubArea BuildSubAreaWithCells(SubAreaConfiguration conf)
        {
            var outRes = new SubArea()
            {
                Id = 0,
                HeightCells = conf.Height,
                LengthCells = conf.Length,
                WidthCells = conf.Width,
                AreaId = 0,
                CellTypeId = conf.CellTypeId,
                Area = _areaState,
            };

            //Fulling SubArea by Cells
            var cells = new List<Cell>(outRes.HeightCells * outRes.WidthCells * outRes.LengthCells);

            for (int heigth = 1; heigth <= outRes.HeightCells; heigth++)
            for (int length = 1; length <= outRes.LengthCells; length++)
            for (int width =  1; width  <= outRes.WidthCells;  width++)
                cells.Add(new Cell(length, heigth, width)
                {
                    SubArea = outRes,
                    SubAreaId = 0
                });

            outRes.Cells = cells;
            return outRes;
        }
    }
}
