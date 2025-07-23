using System.Collections.Generic;


public interface IUsableItem{ 

    void Use();
    string GetName();
    string GetDescription();
    Dictionary<string, string> GetStats();
    void OnEquip();
    void OnUnequip();


}

