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

    public GrabberController grabberController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(transform.position, transform.forward);


        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, isInteractable))
        {
            currentTarget = hitInfo.collider.gameObject;
            if (Input.GetKeyDown(pickBind) && !grabberController.HasItem())
            {
                IUsableItem usableItem = currentTarget.GetComponent<IUsableItem>();
                if (usableItem != null)
                {
                    grabberController.PickUp(currentTarget);
                }
            }
        }
        else
        {
            currentTarget = null;
        }

        if (Input.GetKeyDown(dropBind) && grabberController.HasItem())
        {
            grabberController.Drop();
        }

        if (Input.GetMouseButtonDown(0) && grabberController.HasItem())
        {
            grabberController.UseHeldItem();
        }

        if (Physics.Raycast(ray, out RaycastHit hit, .8f) && Input.GetMouseButtonDown(0))
        {
            GameObject target = hit.collider.gameObject;

            if (grabberController.HasItem())
            {
                GameObject heldItem = grabberController.getHeldObject();

                IUsableOnThings usableOn = heldItem.GetComponent<IUsableOnThings>();

                if (usableOn != null)
                {
                    usableOn.UseOn(target);
                }
            }
        }


    }
}
