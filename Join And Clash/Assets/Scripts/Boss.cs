using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public Animator bossAnim;
    public static Boss boss;
    [SerializeField] private int attackChange;
    [SerializeField] private int danceChange;
    public bool lockTarget, boosIsAlive;
    private Transform target;
    public Slider healhBar;
    public TextMeshProUGUI healthText;
    public int health;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float maxDistance, minDistance;

    private void Start()
    {
        boss = this;
        var enemy = GameObject.FindGameObjectsWithTag("Add");
        foreach (var stickman in enemy)
        {
            enemyList.Add(stickman);
        }
        bossAnim = GetComponent<Animator>();
        boosIsAlive = true;
        healhBar.value = healhBar.maxValue = health = 100;
        healthText.text = health.ToString();
    }
    private void Update()
    {
        healhBar.transform.rotation = Quaternion.Euler(healhBar.transform.rotation.x, 0f, healhBar.transform.transform.rotation.y);
        if (enemyList.Count > 0)
        {
            foreach (var stickman in enemyList)
            {
                var stickmanDistance = stickman.transform.position - transform.position;
                if (stickmanDistance.sqrMagnitude < maxDistance * maxDistance && !lockTarget)
                {
                    target = stickman.transform;
                    bossAnim.SetBool("Attack", true);
                    transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
                }
                if (stickmanDistance.sqrMagnitude < minDistance * minDistance)
                {
                    lockTarget = true;
                }
            }
        }
        if (lockTarget)
        {
            var bossRotation = new Vector3(target.position.x,transform.position.y,target.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList.ElementAt(i).GetComponent<Memeber>().member)
                {
                    enemyList.RemoveAt(i);
                }
            }
        } 


        if (enemyList.Count == 0)
        {
            bossAnim.SetBool("Attack", false);
            bossAnim.SetFloat("FightMode", danceChange);
        }

        if (health <= 0 && boosIsAlive)
        {
            Instantiate(deathEffect, new Vector3(transform.position.x,transform.position.y + 2f, transform.position.z), Quaternion.identity);
            gameObject.SetActive(false);
            boosIsAlive = false;
        }
    } 
    public void ChangeBossAttack()
    {
        bossAnim.SetFloat("FightMode", Random.Range(2, attackChange));
    }
}
