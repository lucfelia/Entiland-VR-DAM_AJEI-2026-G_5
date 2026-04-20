using System.Threading;
using UnityEngine;

public class HookDown : MonoBehaviour
{
    public bool isGrabbing =false;
    public GameObject startPoint;
    public GameObject downpoint;

    private Rigidbody rbObjectGrabbed;

    [SerializeField] private GameObject hand;
    private SkinnedMeshRenderer handMesh;

    [SerializeField] private float DownUpTime= 3f;
    [SerializeField] private float BackTime = 3f;
    [SerializeField] private float GrabTime= 3f;

    [SerializeField] private float grabRadius = 1f;
    [SerializeField] private LayerMask grabbableLayer;
    private Transform grabbedObject;
    [SerializeField] private Transform[] raycastPoints = new Transform[4];

    private float timer;
    private Vector3 positionToDown;
    public enum HookState
    {
        None = 0,   
        Down=1,
        Grab =2,
        Up=3,
        ReturnBack=4,
        ReturnLeft=5,
        UnGrab=6,
        Close = 7
    }
    private HookState hookState;

    void Start()
    {
        handMesh = hand.GetComponentInChildren<SkinnedMeshRenderer>();
        isGrabbing = true;
        hookState = HookState.Down;
        positionToDown = transform.localPosition;
    }
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
            case HookState.Close:
                Closing();
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
            handMesh.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, timer / GrabTime));

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
            handMesh.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, timer / GrabTime));
        }
        else
        {
            for (int i = 0; i < raycastPoints.Length; i++)
            {
                Debug.DrawRay(raycastPoints[i].position, Vector3.down * grabRadius, Color.red, 3f);

                if (Physics.Raycast(raycastPoints[i].position, Vector3.down, out RaycastHit hit, grabRadius, grabbableLayer))
                {
                    grabbedObject = hit.transform;

                    foreach (Rigidbody rb in grabbedObject.GetComponentsInChildren<Rigidbody>())
                    {
                        rb.isKinematic = true;
                    }

                    rbObjectGrabbed = grabbedObject.GetComponent<Rigidbody>();
                    grabbedObject.SetParent(hand.transform);
                    Debug.Log("Objeto agarrado: " + grabbedObject.name);
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
            handMesh.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, timer / GrabTime));
        }
        else
        {
            if (grabbedObject != null)
            {
                grabbedObject.SetParent(null);

                foreach (Rigidbody rb in grabbedObject.GetComponentsInChildren<Rigidbody>())
                {
                    rb.isKinematic = false;
                }

                grabbedObject = null;
                rbObjectGrabbed = null;
            }

            timer = 0;
            hookState = HookState.None;
        }
    }
    private void Closing()
    {
        if (timer < GrabTime)
        {
            timer += Time.deltaTime;
            handMesh.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, timer / GrabTime));
        }
        else
        {
            timer=0;
            hookState = HookState.None;
        }
    }
    public void StartGrabbing()
    {
        isGrabbing = true;
        hookState= HookState.Down;
        positionToDown = transform.localPosition;
    }
}
