using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    Player p; // script de contrôle du joueur
    public LayerMask layer; // layer à ignorer pour les raycasts
    [Range(0.05f,1.0f)]
    public float intelligence; // Temps de reaction
    public float randomMax = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Player>();
        // On fait tourner en boucle notre fonction de décision
        InvokeRepeating("TakeDecision",Random.Range(0.2f,1.5f),intelligence);
        InvokeRepeating("Urgence",Random.Range(0.2f,1.5f),intelligence/3);
    }
    void Urgence()
    {
        //s'il y'a un mur à moins de 2 unités ! 
        //Urgence!
    }

    void TakeDecision()
    {
        // Pour stocker la distance des murs de gauche , droite et en face
        RaycastHit2D forwardHit;
        RaycastHit2D rightHit;
        RaycastHit2D leftHit;

        // Calcul de la direction droite par rapport à en face
        Vector2 dirRight = p.GetRightDir();

        // on envoie des rayons dans les 3 directions possibles
        forwardHit = Physics2D.Raycast(transform.position,p.dir,Mathf.Infinity,~layer);
        rightHit = Physics2D.Raycast(transform.position,dirRight,Mathf.Infinity,~layer);
        leftHit = Physics2D.Raycast(transform.position,-dirRight,Mathf.Infinity,~layer);

        // Si il y'a un mur devant nous
        if (forwardHit.distance < Random.Range(6,10))// il y'a un mur à moins de 8 unités
        {
            // on tourne dans la direction où il y'a le plus d'espace
            if(leftHit.distance > rightHit.distance)
            {
                p.TurnLeft();
            }
            else
            {
                p.TurnRight();
            }
        }
        // sinon pas de mur
        else
        {
            // il y'a entre 10% et 30% de chance que l'IA décide de tourner dans une direction
            if(Random.value < Random.Range(.1f,randomMax))
            {
                // S'il y'a suffisament d'espace dans la direction souhaitée
                //On tourne
                if(leftHit.distance>6)
                {
                    p.TurnLeft();
                }
                else if(rightHit.distance > 6)
                {
                    p.TurnRight();
                }
                // sinon on fait rien
            }
        }

    }
}
