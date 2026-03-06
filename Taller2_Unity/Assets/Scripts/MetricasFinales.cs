using TMPro;
using UnityEngine;

public class MetricasFinales : MonoBehaviour
{
    public TMP_Text tiempoText;
    public TMP_Text monedasText;
    public TMP_Text pocionesText;
    public TMP_Text vidasText;
    public TMP_Text puntosText;
    void Start()
    {
        var gm = GameManager.Instance;

        tiempoText.text =  gm.GlobalTime.ToString("F2");
        monedasText.text = gm.scoreCoin.ToString();
        pocionesText.text =  gm.scorePoison.ToString();
        vidasText.text =  gm.playerLives.ToString();
        puntosText.text =  (gm.scoreCoin + gm.scorePoison).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
