using UnityEngine;

public class KeyholScript : MonoBehaviour
{
    [Header("Target Door and key")]
    public GameObject targetDoor;
    public GameObject requestedKey;
    public bool isOpen = false;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Insert(GameObject key)
    {
        if(isOpen)
        {
            return;
        }

       if (requestedKey == key)
       {
          animator = GetComponentInParent<Animator>();
          animator.SetTrigger("Open");
          isOpen = true;
       }
       
    }
}
 
