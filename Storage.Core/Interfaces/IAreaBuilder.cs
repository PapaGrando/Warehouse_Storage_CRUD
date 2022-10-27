using Storage.Core.Models.Storage;

namespace Storage.Core.Interfaces
{
    public interface IAreaBuilder
    {
        IAreaBuilder AddSubArea(SubAreaConfiguration conf, int count = 1);
        IAreaBuilder ApplyConfig(AreaConfiguration config);
        Area Build();
    }
}
