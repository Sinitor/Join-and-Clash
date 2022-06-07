using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss") && Random.Range(0,2) == 1)
        {
            Boss.boss.health--;
            Boss.boss.healthText.text = Boss.boss.health.ToString();
            Boss.boss.healhBar.value = Boss.boss.health;
        }
    }
}
