namespace CheckoutApp.DataAccess.Exceptions;

[Serializable]
public class RepositoryException : Exception
{
    public RepositoryException()
    {
    }

    public RepositoryException(string message)
        : base(message)
    {
    }

    public RepositoryException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class EntityNullRepositoryException : RepositoryException
{
    public EntityNullRepositoryException() : base("Provided entity can not be null.")
    {
    }
}

public class AddAsyncRepositoryException : RepositoryException
{
    public AddAsyncRepositoryException(Exception innerException)
        : base($"An error occurred while trying to add a new entity: {innerException.Message}", innerException)
    {
    }
}

public class UpdateAsyncRepositoryException : RepositoryException
{
    public UpdateAsyncRepositoryException(Exception innerException)
        : base($"An error occurred while trying to update a new entity: {innerException.Message}", innerException)
    {

    }
}

