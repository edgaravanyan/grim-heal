using UnityEngine;

namespace Data
{
    /// <summary>
    /// Base abstract class for handling ScriptableObject data.
    /// </summary>
    /// <typeparam name="T">Type of ScriptableObject data.</typeparam>
    public abstract class Data<T> where T : ScriptableObject
    {
        // Protected field to hold the ScriptableObject data.
        protected T scriptableObjectData;

        /// <summary>
        /// Constructor for initializing the Data with ScriptableObject data.
        /// </summary>
        /// <param name="data">The ScriptableObject data to be handled.</param>
        protected Data(T data)
        {
            scriptableObjectData = data;
        }
    }
}