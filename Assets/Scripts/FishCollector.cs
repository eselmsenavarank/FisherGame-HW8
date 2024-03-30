using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollector : MonoBehaviour
{

    [SerializeReference] GameObject fishPrefab;
    public List <GameObject> fishList;
    bool thereIsFish;

    public bool isThereFish { get { return thereIsFish;  } }


    // Start is called before the first frame update
    void Start()
    {
         fishList = new List<GameObject>();
        thereIsFish = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn Fish
        if (Input.GetMouseButtonDown(1)) // Right-click to spawn fish
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            GameObject fish = Instantiate(fishPrefab, worldPosition, Quaternion.identity);
            fishList.Add(fish);
            thereIsFish = true;
        }

    }

    public int closestFish(Vector3 shipPos)
    {
        float closest = Vector3.Distance(fishList[0].transform.position, shipPos); //set the first fish in the list as a closest
        int closestIndex = 0; //index of the closest fish

        GameObject closestObject = null;
        for (int i = 0; i < fishList.Count; i++)  //list of gameObjects to search through
        {
            float dist = Vector3.Distance(fishList[i].transform.position, shipPos);
            if (dist < closest)
            {
                closest = dist;
                closestIndex = i;
            }
        }
        return closestIndex;

    }

    // Collect fish
    public GameObject getFish(Vector3 shipPos)
    {
        if (fishList.Count == 0)
        {
            return null;
        }
        else
        {
            int closestFishIndex = closestFish(shipPos);
            GameObject closestFishObj = fishList[closestFishIndex];
            fishList.RemoveAt(closestFishIndex);
            thereIsFish = fishList.Count > 0; // Update the thereIsFish flag based on remaining fish
            return closestFishObj;
        }
    }



}
