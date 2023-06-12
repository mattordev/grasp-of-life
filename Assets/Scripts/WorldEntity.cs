using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife.World
{
    /// <summary>
    /// The base class for any world entity in the game.
    ///  
    /// All world objects will have this class, storing a transform reference and a name 
    /// </summary>
    public class WorldEntity : MonoBehaviour
    {
        public Transform entityTransform;

        private static string _entityName;
        public static string EntityName
        {
            get { return _entityName; }
            set
            {
                // Check the message len
                if (value.Length > 50)
                {
                    // Warn that the status message might be too long
                    Debug.LogWarning($"Entity: {value}'s name is too long! It could display incorrectly!");
                    // Set the status variable
                    _entityName = value;
                }
                else
                {
                    // Set the status variable
                    _entityName = value;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Set the name to the objects name
            EntityName = gameObject.name;
            // Set the entity transform
            entityTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
