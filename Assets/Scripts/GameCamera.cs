using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    // Use this for initialization
    private GameObject cameraTarget;
    public Transform target1;
    public Transform target2;
    public float damping = .1f;
    public float lookAheadFactor = 0f;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private WorldController worldControl; //variable for accessing the world controller
    private bool setupComplete = false;

    // Use this for initialization
    private void Start()
    {
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>(); // links the world controller script to the world controll attribut to give access to world controller data
        cameraTarget = new GameObject();
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
        if (!setupComplete && ((Application.loadedLevelName == "Test Level" && !worldControl.runTestSetup) || (Application.loadedLevelName == "Level 1" && !worldControl.runLevel1Setup))) //setup for world controller is complete but not for camera
        {
            if (worldControl.p2Active)
            {
                target2 = worldControl.P2.transform;
                cameraTarget.transform.position = worldControl.P2.transform.position;
            }
            if (worldControl.p1Active)
            {
                target1 = worldControl.P1.transform;
                cameraTarget.transform.position = worldControl.P1.transform.position;
            }
            m_LastTargetPosition = cameraTarget.transform.position;
            m_OffsetZ = (transform.position - cameraTarget.transform.position).z;
            setupComplete = true;
        }
        if (setupComplete) {
            if (target1 != null && target2 != null)//2 player
            {
                cameraTarget.transform.position = new Vector3((target1.position.x + target2.position.x) / 2, (target1.position.y + target2.position.y) / 2); //this is problem
                //calculate size of screen needed
                float xDis = Mathf.Abs(target1.position.x - cameraTarget.transform.position.x);
                float yDis = Mathf.Abs(target1.position.y - cameraTarget.transform.position.y);
                if (xDis > yDis)//x direction is bigger
                {
                    GetComponent<Camera>().orthographicSize = xDis * .9f;
                }
                else//y direction is bigger
                {
                    GetComponent<Camera>().orthographicSize = yDis + 2;
                }
                if(GetComponent<Camera>().orthographicSize < 5) //the two charater are close to each other
                {
                    GetComponent<Camera>().orthographicSize = 5;
                }

            }
            else if (target1 != null)//p1 player
            {
                cameraTarget.transform.position = target1.position;
            }
            else if (target2 != null)//p2 player
            {
                cameraTarget.transform.position = target2.position;
            }

            if (cameraTarget != null)
            {
                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (cameraTarget.transform.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = cameraTarget.transform.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                transform.position = newPos;

                m_LastTargetPosition = cameraTarget.transform.position;
            }
        }
    }
}
