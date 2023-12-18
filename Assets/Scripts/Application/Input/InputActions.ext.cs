using VContainer.Unity;

namespace Application.Input
{
    /// <summary>
    /// Partial class representing input actions in the application.
    /// </summary>
    public partial class @InputActions : IInitializable
    {
        /// <summary>
        /// Initializes the input actions. This method is called automatically
        /// by VContainer during the container setup.
        /// </summary>
        void IInitializable.Initialize()
        {
            // Enable input actions during initialization.
            Enable();
        }
    }
}