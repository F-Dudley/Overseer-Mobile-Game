using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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
    }
}
