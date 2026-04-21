namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task CompleteAsync(CancellationToken cancellationToken = default);
    }
}

