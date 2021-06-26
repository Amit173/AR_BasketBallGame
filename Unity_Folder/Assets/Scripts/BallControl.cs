using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;


[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public static BallControl instance;
    // This is the force of the throw
    public float m_ThrowForce = 100f;

	// X and Y axis damping factors for the throw direction
	public float m_ThrowDirectionX = 0.17f ,m_ThrowDirectionY = 0.67f;

	// Offset of the ball's position in relation to camera's position
	public Vector3 m_BallCameraOffset = new Vector3(0f, -1.4f, 3f);

	// The following variables contain the state of the current throw
	private Vector3 startPosition ,direction ;
	private float startTime ,endTime ,duration;
	private bool directionChosen = false ,throwStarted = false;

	[SerializeField]
	GameObject ARCam;

	[SerializeField]
	ARSessionOrigin m_SessionOrigin;

    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private TextMeshProUGUI FinalScore;

    [Header("Pointers")]
    [SerializeField]
    private GameObject NetPoint;
    [SerializeField]
    private GameObject Board;

    [SerializeField]
    private GameObject GameOverPanel;

    private int ThrowCount = 0;
    private int Score = 00;

    Rigidbody rb;

	private void Start()
    {   
        ThrowCount = 0;
        Debug.Log("ThrowCount" + ThrowCount);
        rb = gameObject.GetComponent<Rigidbody>();
		m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
		ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
		transform.parent = ARCam.transform;
        NetPoint = PlacementScirpt.spawnedObject.transform.Find("Hoop").Find("Plane01").Find("NetPoint").gameObject;
        Board = PlacementScirpt.spawnedObject.transform.Find("Hoop").Find("Board").gameObject;
		// ResetBall();
	}

	private void Update()
	{
		if(ThrowCount >= 3)
		{
            GameOverPanel.SetActive(true);
        }
        else
        {

            // We've started the touch of the screen, which will start collecting info about the ball throw
            if (Input.GetMouseButtonDown(0))
            { // Works for both Mouse and Touch on Mobile, when we press/touch
                startPosition = Input.mousePosition;
                startTime = Time.time;
                throwStarted = true;
                directionChosen = false;
            }
            // We've ended the touch of the screen, which will end collecting info about the ball throw
            else if (Input.GetMouseButtonUp(0))
            { // Works for both Mouse and Touch, when we release click/touch
                if (Input.mousePosition.y > startPosition.y)
                {
                    Debug.Log("up swipe");
                    endTime = Time.time;
                    duration = endTime - startTime;
                    direction = Input.mousePosition - startPosition;
                    directionChosen = true;
                    // transform.GetComponent<Rigidbody>().AddForce(0f, currentSwipe.x, currentSwipe.y);
                    // transform.GetComponent<Rigidbody>().useGravity = true;
                }
            }

            // Direction was chosen, which will release/throw the ball
            if (directionChosen)
            {
                rb.mass = 1;
                rb.useGravity = true;

                rb.AddForce(
                    ARCam.transform.forward * m_ThrowForce / duration +
                    ARCam.transform.up * direction.y * m_ThrowDirectionY +
                    ARCam.transform.right * direction.x * m_ThrowDirectionX);

                startTime = 0.0f;
                duration = 0.0f;

                startPosition = new Vector3(0, 0, 0);
                direction = new Vector3(0, 0, 0);

                throwStarted = false;
                directionChosen = false;
            }

            // 5 seconds after throwing the ball, we reset it's position
            if (Time.time - endTime >= 5 && Time.time - endTime <= 6)
                ResetBall();
        }

        checkHighestScore();

    }

	public void ResetBall()
	{
        //the method will reset the position of the ball
        ThrowCount++;
        rb.mass = 0;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		endTime = 0.0f;

        Vector3 ballPos = ARCam.transform.position + ARCam.transform.forward * m_BallCameraOffset.z + ARCam.transform.up * m_BallCameraOffset.y;
		transform.position = ballPos;
        //these collider will enable true again for the new score addition
        NetPoint.transform.GetComponent<SphereCollider>().enabled = true;
        Board.transform.GetComponent<SphereCollider>().enabled = true;
	}


    //I'm using colliders for the scoring part
    void OnCollisionEnter(Collision collision)
	{
        Debug.Log(collision.gameObject.name);
        
        //this is the net score when the ball goes into the net
        if(collision.gameObject.name == "NetPoint")
		{
            Debug.Log(collision.gameObject.name);
            Score += 5;
            NetPoint.transform.GetComponent<SphereCollider>().enabled = false;
            Board.transform.GetComponent<SphereCollider>().enabled = false;
        }
		//if ball touchs the board
		if(collision.gameObject.name == "Board")
		{
            Score += 2;
            Board.transform.GetComponent<SphereCollider>().enabled = false;
            NetPoint.transform.GetComponent<SphereCollider>().enabled = false;
        }
        ScoreText.text = Score.ToString();
        FinalScore.text = Score.ToString();
    }

    //this method to update the higesht score....
    public void checkHighestScore()
    {
        if (ScoreChecking.HighestScore <= Score)
        {
            ScoreChecking.HighestScore = Score;
        }
    }

    // public void ReSetGame()
    // {

    //     GameOverPanel.SetActive(false);
    //     ResetBall();
    //     PlacementScirpt.spawnedObject = null;
    //     Score = 0;
    // }

}
