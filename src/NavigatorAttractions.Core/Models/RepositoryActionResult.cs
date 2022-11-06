namespace NavigatorAttractions.Core.Models
{
    public class RepositoryActionResult<T>
        where T : class
    {
        public RepositoryActionResult(T entity)
        {
            Entity = entity;
        }

        public RepositoryActionResult(T entity, string status)
        {
            Entity = entity;
            Status = status;
        }

        public RepositoryActionResult(T entity, string status, Exception exception)
            : this(entity, status)
        {
            Exception = exception;
        }

        public T Entity { get; private set; }

        public string Status { get; private set; }

        public Exception Exception { get; private set; }
    }
}
