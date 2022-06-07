using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool moveTouch, gameState, attackTheBoss;
    private Vector3 direction;
    public List <Rigidbody> rb =  new List<Rigidbody>();
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    public Transform road;
    public static Player player;

    private void Start()
    {
        player = this;
        rb.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        gameState = true;
    }
    private void Update()
    {
        if (gameState)
        {
            if (moveTouch)
            {
                direction.x = Mathf.Lerp(direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);
                direction = Vector3.ClampMagnitude(direction, 1f);
                road.position = new Vector3(-3.71f, 0.52f, Mathf.SmoothStep(road.position.z, -150f, Time.deltaTime * roadSpeed));

                foreach (var anim in rb)
                {
                    anim.GetComponent<Animator>().SetFloat("Run", 1);
                }
            }
            else
            {
                foreach (var anim in rb)
                {
                    anim.GetComponent<Animator>().SetFloat("Run", 0);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                moveTouch = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                moveTouch = false;
            }  
            foreach (var stickrb in rb)
            {
                if (stickrb.velocity.magnitude > 0.5f)
                {
                    stickrb.rotation = Quaternion.Slerp(stickrb.rotation, Quaternion.LookRotation(stickrb.velocity, Vector3.up), Time.deltaTime * velocity);
                }
                else
                {
                    stickrb.rotation = Quaternion.Slerp(stickrb.rotation, Quaternion.identity, Time.deltaTime * velocity);
                }
            }
        }
        else
        {
            if (!Boss.boss.boosIsAlive)
            {
                foreach (var stickman in rb)
                {
                    stickman.GetComponent<Animator>().SetFloat("FightMode", 4);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (gameState)
        {
            if (moveTouch)
            {
                Vector3 displacement = new Vector3(direction.x, 0f, 0f) * Time.fixedDeltaTime;
                foreach (var stickrb in rb)
                {
                    stickrb.velocity = new Vector3(direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) + displacement;
                }
            }
            else
            {
                foreach (var stickrb in rb)
                {
                    stickrb.velocity = Vector3.zero;
                }
            }
        }
    }
}
