using System;

namespace UtilityDelta.Server.Dependencies
{
    /// <summary>
    ///     This interface wraps the AutoFac dependency injection implementation
    /// </summary>
    public interface IDependencyLocator : IDisposable
    {
        /// <summary>
        /// Get an instance of a concrete class with a provided interface
        /// </summary>
        /// <typeparam name="T">Interface to get concrete class for</typeparam>
        /// <returns></returns>
        T GetService<T>();
    }
}