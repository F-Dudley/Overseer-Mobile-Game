using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public LayerMask groundLayer;
    public bool placedObstacle = false;
    public GameObject obstacle;

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameActive && Input.GetMouseButtonDown(0)) {
            Debug.Log("Shooting...Pew Pew");
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                if(hit.transform.gameObject.tag == "Enemy") {
                    Destroy(hit.transform.root.gameObject);
                    GameManager.instance.addScore.Invoke();
                }
            }
        }
        else if(!GameManager.instance.gameActive && Input.GetMouseButtonDown(0)) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                if(placedObstacle && hit.transform.gameObject.tag == "Obstacle") {
                    Destroy(hit.transform.root.gameObject);
                    GameManager.instance.UpdateNavMesh();

                    placedObstacle = false;
                }
                else if(!placedObstacle && hit.transform.gameObject.tag == "Ground") {
                    Instantiate(obstacle, hit.point, Quaternion.identity);
                    GameManager.instance.UpdateNavMesh();

                    placedObstacle = true;
                }
            }
        }
        
    }
}
