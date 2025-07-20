using Unity.VisualScripting;
using UnityEngine;

public class RayCasterCamera : MonoBehaviour
{

    private GameObject currentTarget;
    [Header("Interact Ray")]
    public float rayDistance;
    public LayerMask isInteractable;

    [Header("Keybinds")]
    public KeyCode pickBind = KeyCode.F;
    public KeyCode dropBind = KeyCode.Q;
    public KeyCode useBind = KeyCode.Mouse0;

    public GrabberController GrabberController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, isInteractable))
        {
            HandleRayCastHit(hitInfo);
            if (Input.GetKeyDown(pickBind) && !GrabberController.HasItem())
            {
                IUsableItem usableItem = currentTarget.GetComponent<IUsableItem>();
                if (usableItem != null)
                {
                    GrabberController.PickUp(currentTarget);
                }
            }
        }
        else
        {
            currentTarget = null;
        }

        if(Input.GetKeyDown(dropBind) && GrabberController.HasItem())
        {
            GrabberController.Drop();
        }

        if(Input.GetMouseButtonDown(0) && GrabberController.HasItem()) {
            GrabberController.UseHeldItem();
        }

    }

    void HandleRayCastHit(RaycastHit hitInfo)
    {
        currentTarget = hitInfo.collider.gameObject;
        Debug.Log(currentTarget.GetComponent<MonoBehaviour>());


    }
}
