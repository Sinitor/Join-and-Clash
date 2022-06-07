using System.Linq;
using UnityEngine;

public class Recruit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Add"))
        {
            Player.player.rb.Add(collision.collider.GetComponent<Rigidbody>());
            collision.transform.parent = null;
            collision.transform.parent = Player.player.transform;
            collision.gameObject.GetComponent<Memeber>().member = true;

            if (!collision.collider.gameObject.GetComponent<Recruit>())
            {
                collision.collider.gameObject.AddComponent<Recruit>();
            }

            collision.collider.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material =
                Player.player.rb.ElementAt(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        }
    }
}
