using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IUsableItem
{

    public string Name;
    public string description;
    public GameObject targetDoor;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public string GetDescription()
    {
        return description;
    }

    public string GetName()
    {
        return name;
    }

    public Dictionary<string, string> GetStats()
    {
        return new Dictionary<string, string>
        {
            {"Opens", targetDoor != null ? targetDoor.name : "Unknow"},
        };
    }

    public void OnEquip()
    {
        Debug.Log("Key Equipped");
    }

    public void OnUnequip()
    {
        Debug.Log("Key Unequipped");
    }

    public void Use()
    {
        Debug.Log("Opening:" + targetDoor.name);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
