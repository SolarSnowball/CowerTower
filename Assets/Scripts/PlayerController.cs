using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator anim;

    GameObject Player;
    GameObject RayRot;

    SpriteRenderer playerSprite;

    float speed = 10.0f;
    public Vector2 target;
    public Vector2 target2;
    public Vector2 position;

    RaycastHit2D objectHit;

    int attackDir;
    int playerHealth;
    public bool playerTurn;
    public bool playerMoved;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        RayRot = GameObject.Find("RaycastRot");
        playerSprite = Player.GetComponentInChildren<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();

        target = new Vector2(0.5f, 0.5f);

        playerTurn = anim.GetBool("playerTurn");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        position = Player.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if (anim.GetBool("Attacking") == true)
        {

        }
        else if (playerMoved == false && playerTurn == true)
        {
            if (Input.GetKeyDown("w"))
            {
                objectHit = Physics2D.Raycast(RayRot.transform.position, Vector2.up, 1.0f);
                if (objectHit.collider == null)
                {
                    target.y = target.y + 1;
                }
                attackDir = 0;
                playerTurn = false;
            }
            if (Input.GetKeyDown("s"))
            {
                objectHit = Physics2D.Raycast(RayRot.transform.position, Vector2.down, 1.0f);
                if (objectHit.collider == null)
                {
                    target.y = target.y - 1;
                }
                attackDir = 1;
                playerTurn = false;
            }
            if (Input.GetKeyDown("a"))
            {
                objectHit = Physics2D.Raycast(RayRot.transform.position, Vector2.left, 1.0f);
                if (objectHit.collider == null)
                {
                    target.x = target.x - 1;
                }
                playerSprite.flipX = true;
                attackDir = 2;
                playerTurn = false;
            }
            if (Input.GetKeyDown("d"))
            {
                objectHit = Physics2D.Raycast(RayRot.transform.position, Vector2.right, 1.0f);
                if (objectHit.collider == null)
                {
                    target.x = target.x + 1;
                }
                playerSprite.flipX = false;
                attackDir = 3;
                playerTurn = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Attacking", true);
                Invoke("WaitForAttack", 0.5f);
            }

            anim.SetInteger("attackDir", attackDir);
            anim.SetBool("playerTurn", playerTurn);
        }

        if(!playerTurn)
        {

        }

        if (Vector2.Distance(position, target2) == 0)
        {
            playerMoved = true;
        }
        else
        {
            playerMoved = false;
        }

        if (playerTurn == false && playerMoved == true)
        {
            EnemyTurn();
        }

    }
    void WaitForAttack()
    {
        anim.SetBool("Attacking", false);
    }

    void EnemyTurn()
    {
        Invoke("PlayerTurn", 1);
    }

    void PlayerTurn()
    {
        playerTurn = true;
        playerMoved = false;
    }
}
