using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGenerator : MonoBehaviour
{

    public int Total = 8;
    public GameObject food;

    public GameObject FoodParent;

    private List<Vector3> FoodPosition = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        PlaneFood1(11);
        PlaneFood1(-11);
        PlaneFood2(11);
        PlaneFood2(-11);
        PlaneFood3(11);
        PlaneFood3(-11);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create foods on up & bottom sides
    void PlaneFood1(int height)
    {
        //create foods on plane1
        for(int i=0; i< Total; i++)
        {
            Vector3 food_pos = new Vector3(Random.Range(-9,9), height, Random.Range(-9,9));
            for(int j=0; j<i; j++){
                if( j != i)
                {
                    if(Vector3.Distance(food_pos, FoodPosition[j]) < 8)
                    {
                        food_pos = new Vector3(Random.Range(-9,9), height, Random.Range(-9,9));
                    }
                }

            }
            GameObject body = Instantiate(food, food_pos, food.transform.rotation, FoodParent.transform);
            FoodPosition.Add(body.transform.position);
            //print(FoodPosition[i]);
        }
        FoodPosition.Clear();
    }

    // Create foods on left & right sides
    void PlaneFood2(int height)
    {
        //create foods on plane1
        for(int i=0; i< Total; i++)
        {
            Vector3 food_pos = new Vector3(height, Random.Range(-9,9), Random.Range(-9,9));
            for(int j=0; j<i; j++){
                if( j != i)
                {
                    if(Vector3.Distance(food_pos, FoodPosition[j]) < 8)
                    {
                        food_pos = new Vector3(height, Random.Range(-9,9), Random.Range(-9,9));
                    }
                }

            }
            GameObject body = Instantiate(food, food_pos, food.transform.rotation, FoodParent.transform);
            FoodPosition.Add(body.transform.position);
            //print(FoodPosition[i]);
        }
        FoodPosition.Clear();
    }

    // Create foods on front & back sides
    void PlaneFood3(int height)
    {
        //create foods on plane1
        for(int i=0; i< Total; i++)
        {
            Vector3 food_pos = new Vector3(Random.Range(-9,9), Random.Range(-9,9),height);
            for(int j=0; j<i; j++){
                if( j != i)
                {
                    if(Vector3.Distance(food_pos, FoodPosition[j]) < 8)
                    {
                        food_pos = new Vector3(Random.Range(-9,9), Random.Range(-9,9),height);
                    }
                }

            }
            GameObject body = Instantiate(food, food_pos, food.transform.rotation, FoodParent.transform);
            FoodPosition.Add(body.transform.position);
            //print(FoodPosition[i]);
        }
        FoodPosition.Clear();
    }
}
