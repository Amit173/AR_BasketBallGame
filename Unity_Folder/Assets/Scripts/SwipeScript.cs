using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    public GameObject _Ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe_Mouse();
    }

    // public void Swipe()
    // {
    //     if (Input.touches.Length > 0)
    //     {
    //         Touch t = Input.GetTouch(0);
    //         if (t.phase == TouchPhase.Began)
    //         {
    //             //save began touch 2d point
    //             firstPressPos = new Vector2(t.position.x, t.position.y);
    //             Debug.Log("Touch Began");
    //         }
    //         if (t.phase == TouchPhase.Ended)
    //         {
    //             //save ended touch 2d point
    //             secondPressPos = new Vector2(t.position.x, t.position.y);

    //             //create vector from the two points
    //             currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

    //             //normalize the 2d vector
    //             currentSwipe.Normalize();

    //             //swipe upwards
    //             if (currentSwipe.y > 0 || currentSwipe.x > -0.5f  || currentSwipe.x < 0.5f)
    //             {
    //                 Debug.Log("up swipe");
    //             }
    //             //swipe down
    //             if (currentSwipe.y < 0 || currentSwipe.x > -0.5f || currentSwipe.x < 0.5f)
    //             {
    //                 Debug.Log("down swipe");
    //             }
    //             //swipe left
    //             if (currentSwipe.x < 0 || currentSwipe.y > -0.5f || currentSwipe.y < 0.5f)
    //             {
    //                 Debug.Log("left swipe");
    //             }
    //             //swipe right
    //             if (currentSwipe.x > 0 || currentSwipe.y > -0.5f ||  currentSwipe.y < 0.5f)
    //             {
    //                 Debug.Log("right swipe");
    //             }
    //         }
    //     }
    // }

    // Vector2 firstPressPos;
    // Vector2 secondPressPos;
    // Vector2 currentSwipe;

    public void Swipe_Mouse()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Debug.Log("Touch Began");
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            Debug.Log(currentSwipe);

            //normalize the 2d vector
            // currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                _Ball.transform.GetComponent<Rigidbody>().AddForce(0f, currentSwipe.x, currentSwipe.y);
                _Ball.transform.GetComponent<Rigidbody>().useGravity = true;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
            }
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            targetObject = hit.collider.gameObject;
        }
        return targetObject;
    }
}
