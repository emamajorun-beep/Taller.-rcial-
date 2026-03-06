using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    

    public void SumarVida(int cantidad)
    {
        GameManager.Instance.AddLives(cantidad);
    }

    public void RestarVida(int cantidad)
    {
        GameManager.Instance.AddLives(-cantidad);

        if (GameManager.Instance.playerLives <= 0)
        {
            Debug.Log("El jugador ha muerto");
          
        }
    }
}
