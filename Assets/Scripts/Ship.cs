using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Ship : MonoBehaviour
{
    
    Rigidbody2D rb;
    BoxCollider2D collider;
    GameObject currentFish;
    FishCollector fish;

    // Start is called before the first frame update
    void Start()
    {
        fish = Camera.main.GetComponent<FishCollector>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Ship moves towards to the next fish
    void moveShip(Vector3 posFish)
    {
        Vector3 shipDirection = (posFish - transform.position).normalized;
        rb.AddForce(shipDirection * 3, ForceMode2D.Impulse);

    }

    //Ativate the fish
    IEnumerator OnMouseDown()
    {
        Debug.Log("Ship OnMouseDown triggered.");
        while (fish.isThereFish)
        {
            currentFish = fish.getFish(transform.position);
            if (currentFish != null)  // Check if currentFish is not null before proceeding
            {
                moveShip(currentFish.transform.position);
                yield return new WaitUntil(() => currentFish == null);
            }
            else
            {
                yield break; // Exit the coroutine if no fish is available to move towards
            }
        }
    }


    //Collect fish
    void OnTriggerStay2D(Collider2D other)
    {
         if(other != null && other.gameObject == currentFish)
        {
            Destroy(currentFish);
            currentFish = null;


            rb.velocity = Vector2.zero;
        }
    }
}
