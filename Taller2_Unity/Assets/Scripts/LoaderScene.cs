using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenes : MonoBehaviour
{
    
    void Start()
    {

    }

    void Update()
    {

    }
    public void lectorEscena(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}

