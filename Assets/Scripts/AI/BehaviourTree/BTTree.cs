using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife.World.AI.BehaviourTree
{
    /// <summary>
    /// Upon playing, this class sets up the behaviour tree based on what is passed into the BTTree.SetupTree() function.
    /// then, if the tree exists, it gets evaluated constantly here.
    /// </summary>
    public abstract class BTTree : MonoBehaviour
    {
        private BTNode _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract BTNode SetupTree();
    }
}
