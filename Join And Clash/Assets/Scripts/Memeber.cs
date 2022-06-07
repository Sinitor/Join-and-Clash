using System.Linq;
using UnityEngine;

public class Memeber : MonoBehaviour
{
    [SerializeField] private Animator characterAnim;
    [SerializeField] private GameObject deathEffect;
    private Transform boss;
    public int health;
    [SerializeField] private float minDistance, maxDistance, moveSpeed;
    public bool fight, member;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        characterAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); 
        capsuleCollider = GetComponent<CapsuleCollider>();
        boss = GameObject.FindWithTag("Boss").transform;
        health = 4;
    }

    private void Update()
    {
        var bossDistance = boss.position - transform.position;

        if (!fight)
        {
            if (bossDistance.sqrMagnitude <= maxDistance * maxDistance)
            {
                Player.player.attackTheBoss = true;
                Player.player.gameState = false;
            }
            if (Player.player.attackTheBoss && member)
            {
                transform.position = Vector3.MoveTowards(transform.position, boss.position, moveSpeed * Time.deltaTime);
                var stickmanRotation = new Vector3(boss.position.x, transform.position.y, boss.position.z) - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stickmanRotation, Vector3.up), 10f * Time.deltaTime);
                characterAnim.SetFloat("Run", 1f);
                rb.velocity = Vector3.zero;
            }
        }
        if (bossDistance.sqrMagnitude <= minDistance * minDistance)
        {
            fight = true;
            var stickmanRotation = new Vector3(boss.position.x, transform.position.y, boss.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stickmanRotation, Vector3.up), 10f * Time.deltaTime);
            characterAnim.SetBool("Attack", true); 
            minDistance = maxDistance;
            rb.velocity = Vector3.zero;
        }
        else
        {
            fight = false;
        } 
    }
    public void AttackMode()
    {
        characterAnim.SetFloat("FightMode", Random.Range(0, 3));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            health--;
            if (health <= 0)
            {
                Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z), Quaternion.identity); 

                if (gameObject.name != Player.player.rb.ElementAt(0).name)
                {
                    gameObject.SetActive(false);
                    transform.parent = null;
                }
                else
                {
                    capsuleCollider.enabled = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                }
                for (int i = 0; i < Boss.boss.enemyList.Count; i++)
                {
                    if (Boss.boss.enemyList.ElementAt(i).name == gameObject.name)
                    {
                        Boss.boss.enemyList.RemoveAt(i);
                        break;
                    }
                }
                Boss.boss.lockTarget = false;
            }
        }
        if (collision.collider.CompareTag("Traps"))
        {
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z), Quaternion.identity,Player.player.road);

            gameObject.SetActive(false);
            transform.parent = null;

            for (int i = 0; i < Boss.boss.enemyList.Count; i++)
            {
                if (Boss.boss.enemyList.ElementAt(i).name == gameObject.name)
                {
                    Boss.boss.enemyList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
