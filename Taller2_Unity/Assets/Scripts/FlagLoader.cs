using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Timer timerPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);

            GameManager.Instance.TotalTime(timerPanel.GetCurrentTime());
        }
    }
}

