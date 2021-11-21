using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    Rigidbody2D rb; //rigidbody du joueur

    public int speed; // vitesse de déplacement

    Vector2 dir; // direction

    GameObject wallPrefab; // Mur à instancier
    Vector2 lastPos;
    Collider2D lastWallCol;
    bool canActivateBoost = true;
    public string playerName;
    Cam cam;
    GameManager gm;
    [HideInInspector]
    public bool isAlive = true;
    Vector2 initialPos;
    private void Awake() {
        initialPos = transform.position;
        cam = Camera.main.GetComponent<Cam>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector2.up;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed  * gm.gameSpeed;
        // Récupération dynamique du mur à instancier
        wallPrefab = Resources.Load("wall"+gameObject.tag) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeys();
        SetLastWallSize(lastWallCol,lastPos,transform.position);
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if(col != lastWallCol)
        {
            isAlive =false;
            gm.killPlayer();
            cam.PlayBoomSfx();
            cam.Shake(0.7f,0.4f,50f);
            Instantiate(Resources.Load("boom"),transform.position,Quaternion.identity);
            // Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void HandleKeys()
    {
        // Gestion des touches de direction
        // if(Input.GetKeyDown(KeyCode.UpArrow))
        if(Input.GetButtonDown(playerName+"UP"))
        {
            if(dir != Vector2.down)
            {
                dir = Vector2.up;
                CreateWall();
            }
        }
        // else if(Input.GetKeyDown(KeyCode.DownArrow))
        else if(Input.GetButtonDown(playerName+"DOWN"))
        {
            if(dir != Vector2.up)
            {
                dir = Vector2.down;
                CreateWall();

            }
        }
        // else if(Input.GetKeyDown(KeyCode.LeftArrow))
        else if(Input.GetButtonDown(playerName+"LEFT"))
        {
            if(dir != Vector2.right)
            {
                dir = Vector2.left;
                CreateWall();

            }
        }
        // else if(Input.GetKeyDown(KeyCode.RightArrow))
        else if(Input.GetButtonDown(playerName+"RIGHT"))
        {
            if(dir != Vector2.left)
            {
                dir = Vector2.right;
                CreateWall();

            }
        }
        // else if( Input.GetKeyDown(KeyCode.Space))
        else if( Input.GetButtonDown(playerName+"BOOST"))
        {
            if(canActivateBoost)
            {
                StartCoroutine("ActivateBoost");
                GameObject.Find(playerName+"boost").GetComponent<Text>().text = "boost: 0";
            }
        }
        // on applique le mouvement souhaité
        rb.velocity = dir * speed * gm.gameSpeed;
    }
    IEnumerator ActivateBoost()
    {
        canActivateBoost = false;
        speed += 5;
        yield return new WaitForSeconds(3);
        speed -= 5;
        Invoke("ReloadBoost",30);// appella fonction reloadboost au bout de 30 s
    }

    void ReloadBoost()
    {
        canActivateBoost = true;
        GameObject.Find(playerName+"boost").GetComponent<Text>().text = "boost: 1";
    }
    private void CreateWall()
    {
        lastPos = transform.position;
       GameObject go = Instantiate(wallPrefab, transform.position,Quaternion.identity);
       lastWallCol = go.GetComponent<Collider2D>();
    }

    private void SetLastWallSize(Collider2D col, Vector2 posStart, Vector2 posEnd)
    {
        if(col)
        {
            col.transform.position = posStart + (posEnd-posStart) / 2; //milieu
            float size = Vector2.Distance(posEnd,posStart);
            if(posStart.x != posEnd.x)
            {
                col.transform.localScale = new Vector2(size +1,1);
            }else{
                col.transform.localScale = new Vector2(1,size +1);
            }
        }
    }
    
    public void ResetPlayer()
    {
        transform.position = initialPos;
        isAlive = true;
        dir = Vector2.up;
    }
}
