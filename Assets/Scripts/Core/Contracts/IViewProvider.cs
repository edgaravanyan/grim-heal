using System.Threading.Tasks;

namespace Core.Contracts
{
    /// <summary>
    /// Represents an interface for a provider responsible for creating views asynchronously.
    /// </summary>
    /// <typeparam name="TView">The type of view to be created.</typeparam>
    public interface IViewProvider<TView>
    {
        /// <summary>
        /// Asynchronously creates a view of the specified type.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is the created view.</returns>
        Task<TView> CreateViewAsync();
    }
}