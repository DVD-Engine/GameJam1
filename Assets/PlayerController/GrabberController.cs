using UnityEngine;

public class GrabberController : MonoBehaviour
{

    [Header("References")]
    public Transform handContainer;

    [Header("Settings")]
    public float dropForwardForce, dropUpwardForce;

    private GameObject grabbedObject;
    private Rigidbody grabbedRB;
    private Collider grabbedCollider;
    private IUsableItem usableItem;

    [Header("Flags")]
    public static bool isSlotFull;
    public bool isEquipped;

    public bool HasItem()
    {
        return grabbedObject != null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(GameObject item)
    {
        if (isSlotFull)
        {
            Debug.Log("You are already holding an item");
            return;
        }

        usableItem = item.GetComponent<IUsableItem>();
        if(usableItem == null)
        {
            Debug.LogWarning("Item does not implement IUsableItem!");
            return;
        }

        grabbedObject = item;
        grabbedRB = item.GetComponent<Rigidbody>();
        grabbedCollider = item.GetComponent<Collider>();

        if(grabbedRB != null)
        {
            grabbedRB.isKinematic = true;
        }
        if(grabbedCollider != null)
        {
            grabbedCollider.isTrigger = true;
        }

        grabbedObject.transform.SetParent(handContainer);
        grabbedObject.transform.localPosition = new Vector3(0, .4f, 0); 
        grabbedObject.transform.localRotation = Quaternion.Euler(0, 0, 0); 


        usableItem.OnEquip();

        isEquipped = true;
        isSlotFull = true;
    }

    public void Drop()
    {
        if (!HasItem())
        {
            return;
        }

        grabbedObject.transform.SetParent(null);

        if(grabbedRB != null)
        {
            grabbedRB.isKinematic = false;
            grabbedRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

            Vector3 launchForce = handContainer.forward * dropForwardForce + handContainer.up * dropUpwardForce;
            grabbedRB.AddForce(launchForce, ForceMode.VelocityChange);


        }

        if (grabbedCollider != null)
        {
            grabbedCollider.isTrigger = false;
            grabbedRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        }

        usableItem.OnUnequip();

        grabbedObject = null;
        grabbedRB = null;
        grabbedCollider = null;
        usableItem= null;

        isEquipped = false;
        isSlotFull = false;
    }

    public void UseHeldItem()
    {
        if (usableItem != null)
        {
            usableItem.Use();
        }
        else
        {
            Debug.Log("No item to use.");
        }
    }
}
    