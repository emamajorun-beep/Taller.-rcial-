using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public string nameItem;   
    public int itemValue;
  


    public AudioClip itemSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
     

            if (nameItem == "Poison")
            {
                GameManager.Instance.TotalPoison(itemValue);
            }
            else if (nameItem == "Coin")
            {
                GameManager.Instance.TotalCoin(itemValue);
            }
            else if (nameItem == "Heart")
            {
               GameManager.Instance.AddLives(itemValue);
            }

            if (itemSound != null)
            {
                AudioSource.PlayClipAtPoint(itemSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
