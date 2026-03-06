using TMPro;
using UnityEngine;

public class panelResultado : MonoBehaviour
{
    public GameObject panelResultados;
    public Timer timerPanel;



    void Start()
    {
        
        if (panelResultados != null)
            panelResultados.SetActive(false);

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (panelResultados != null)
                panelResultados.SetActive(true);

            GameManager.Instance.TotalTime(timerPanel.GetCurrentTime());
            timerPanel.TimerStop();

        }
    }
}
