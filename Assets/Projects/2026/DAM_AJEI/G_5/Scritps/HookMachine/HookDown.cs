using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class HookDown : MonoBehaviour
{
    public bool isGrabbing =false;
    public GameObject startPoint;
    public GameObject downpoint;

    [SerializeField] private Transform hand;
    [SerializeField] private Transform middlePos;

    private Transform grabbedObject;

    [SerializeField] private float DownUpTime= 3f;
    [SerializeField] private float BackTime = 3f;
    [SerializeField] private float GrabTime= 3f;

    [SerializeField] private float grabRadius = 1f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private Transform[] raycastPoints = new Transform[4];
    private Rigidbody grabbedObjectRb;

    private float timer;
    private Vector3 positionToDown;

    void Start()
    {
        isGrabbing = true;
        hookState = HookState.Down;
        positionToDown = transform.localPosition;
    }
    public enum HookState
    {
        None = 0,   
        Down=1,
        Grab =2,
        Up=3,
        ReturnBack=4,
        ReturnLeft=5,
        UnGrab=6
    }
    private HookState hookState;
    private void Update()
    {
        switch (hookState)
        {
            case HookState.None:
                return;
            case HookState.Down:
                MoveDown();
                return;
            case HookState.Grab: 
                Grab();
                return;
            case HookState.Up:
                MoveUp();
                return;
            case HookState.ReturnBack: 
                ReturnBack();
                return;
            case HookState.ReturnLeft: 
                ReturnLeft();
                return;
            case HookState.UnGrab:
                Ungrab();
                return;
            default:
                return;
        }
    }
    private void MoveDown()
    {
        if (timer < DownUpTime)
        {
            timer += Time.deltaTime;
            float t = timer / DownUpTime;

            float newY = Mathf.Lerp(
                 positionToDown.y,
                 downpoint.transform.localPosition.y,
                 t
             );
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
        }
        else
        {
            hookState = HookState.Grab;
            timer = 0;
        }
    }
    private void Grab()
    {
        if (timer < GrabTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < raycastPoints.Length; i++)
            {
                Debug.DrawRay(raycastPoints[i].position, Vector3.down * grabRadius, Color.red, 3f);

                if (Physics.Raycast(raycastPoints[i].position, Vector3.down, out RaycastHit hit, grabRadius, grabbableLayer))
                {
                    grabbedObject = hit.transform;
                    grabbedObject.SetParent(hand);
                    grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>();
                    grabbedObjectRb.isKinematic = true;
                    grabbedObject.localPosition = middlePos.localPosition;
                    break;
                }
            }

            timer = 0;
            hookState = HookState.Up;
        }
    }
    private void MoveUp()
    {
        if (timer < DownUpTime)
        {
            timer += Time.deltaTime;
            float t = timer / DownUpTime;

            float newY = Mathf.Lerp(
                 downpoint.transform.localPosition.y,
                 startPoint.transform.localPosition.y,
                 t
             );

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
    }
        else
        {
            timer = 0;
            hookState = HookState.ReturnBack;
        }
    }
    private void ReturnBack()
    {
        if (timer < DownUpTime)
        {
            timer += Time.deltaTime;
            float t = timer / DownUpTime;

            float newZ = Mathf.Lerp(
                positionToDown.z,
                startPoint.transform.localPosition.z,
                t
            );

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                newZ
            );
        }
        else
        {
            timer = 0;
            hookState = HookState.ReturnLeft;
        }
    }
    private void ReturnLeft()
    {
        if (timer < BackTime)
        {
            timer += Time.deltaTime;
            float t = timer / BackTime;
            float newX = Mathf.Lerp(
                positionToDown.x,
                startPoint.transform.localPosition.x,
                t
            );


            transform.localPosition = new Vector3(
                newX,
                transform.localPosition.y,
                transform.localPosition.z
            );       
        }
        else
        {
            timer = 0;
            hookState = HookState.UnGrab;
        }
    }
    private void Ungrab()
    {
        if (timer < GrabTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (grabbedObject != null)
            {
                grabbedObject.SetParent(null);
                grabbedObject = null;
                grabbedObjectRb.isKinematic = false;
            }

            timer = 0;
            hookState = HookState.None;
        }
    }
    public void StartGrabbing()
    {
        isGrabbing = true;
        hookState = HookState.Down;
        positionToDown = transform.localPosition;
    }
}
