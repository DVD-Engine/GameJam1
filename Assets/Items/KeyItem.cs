using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IUsableItem, IUsableOnThings
{

    public string keyName;
    public string description;
    public GameObject targetKeyhole;
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
        return keyName;
    }

    public Dictionary<string, string> GetStats()
    {
        return new Dictionary<string, string>
        {
            {"Opens", targetKeyhole != null ? targetKeyhole.name : "Unknow"},
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
        Debug.Log("Use() called but key needs a target!");
    }

    public void UseOn(GameObject gameObject)
    {
       if(targetKeyhole != null)
        
       {
            KeyholScript keyhole = targetKeyhole.GetComponent<KeyholScript>();
            
            keyhole.Insert(this.gameObject);
       }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
