namespace WarehouseCRUD.Storage.Models.Helpers
{
    public class ModalResult
    {
        public ModalResultInfo ResultInfo { get; set; }
        public string Message { get; set; }
    }

    public enum ModalResultInfo
    {
        Info = 0,
        Error = 1,
        Ok = 2
    }
}
