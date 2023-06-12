using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife
{
    public class MainMenu : MonoBehaviour
    {
        public string sceneToLoad;

        public void Play()
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }

        public void Quit()
        {
            Debug.Log("Application quit called");
            Application.Quit();
        }
    }
}
