using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Opens the given scene.
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
