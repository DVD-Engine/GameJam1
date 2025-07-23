using UnityEngine;

public class KeyholScript : MonoBehaviour
{
    [Header("Target Door and key")]
    public GameObject targetDoor;
    public GameObject requestedKey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Insert(GameObject key)
    {
        if (requestedKey != null)
        {
            if (requestedKey == key)
            {
                targetDoor.transform.Translate(Vector3.up * 2f);
            }
        }
    }
}
 
