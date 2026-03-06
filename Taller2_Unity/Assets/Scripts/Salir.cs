using UnityEngine;

public class Salir : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
