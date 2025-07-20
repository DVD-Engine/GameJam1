using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IUsableItem{ 

    void Use();
    string GetName();
    string GetDescription();
    Dictionary<string, string> GetStats();
    void OnEquip();
    void OnUnequip();


}

