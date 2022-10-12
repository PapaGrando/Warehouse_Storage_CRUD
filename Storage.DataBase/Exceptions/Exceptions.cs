namespace Storage.DataBase.Exceptions
{
    public abstract class BaseDataException : Exception
    {
        /// <summary>
        /// Base Exception for interseptoring in repos, etc
        /// DO NOT INCLUDE PRIVACY DATA IN message!!!!!!!
        /// </summary>
        public BaseDataException(string message) : base(message)
        {
        }
    }

    public class NoCascadeDeletionException<T> : BaseDataException
    {
        public NoCascadeDeletionException(string message = null)
            : base(message ?? $"Not allowed cascade deleting for {typeof(T).Name} entity") { }
    }

    public class NotFound<T> : BaseDataException
    {
        public NotFound(int? id)
            : base($"Record {typeof(T).Name} with id: {id} not found.") { }
    }
}
