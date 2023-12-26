using Core.Contracts.Character;

namespace Data.ScriptableData
{
    /// <summary>
    /// Implementation of the ICharacterStats interface using data from a CharacterDataScriptableObject.
    /// </summary>
    public class CharacterData : EntityData<CharacterDataScriptableObject>, ICharacterData
    {

        public CharacterData(CharacterDataScriptableObject data) : base(data) { }


        /// <summary>
        /// Gets the acceleration of the character.
        /// </summary>
        public float Acceleration => scriptableObjectData.acceleration;

        /// <summary>
        /// Gets the deacceleration of the character.
        /// </summary>
        public float DeAcceleration => scriptableObjectData.deAcceleration;
    }
}