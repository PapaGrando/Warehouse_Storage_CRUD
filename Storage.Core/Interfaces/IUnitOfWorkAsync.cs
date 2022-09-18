namespace Storage.Core.Interfaces
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        IAreaRepoAsync Areas { get; init; }
        ISubAreasRepoAsync SubAreas { get; init; }
        ICellRepoAsync Cells { get; init; }
        ICellTypeRepoAsync CellTypes { get; init; }
        IProductRepoAsync Products { get; init; }
        IProductCategoryRepoAsync ProductCategories { get; init; }
        IStorageItemRepoAsync StorageItems { get; init; }

        Task Commit();
    }
}
