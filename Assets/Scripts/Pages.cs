using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    private string _pageName;

    void Start()
    {
        UI ui = FindObjectOfType<UI>();
        _pageName = this.gameObject.name;
        if (ui != null)
        {
            ui.RegisterPage(_pageName, gameObject);
        }
        else
        {
            Debug.LogError("PageManager non trouvé dans la scène.");
        }
    }
}
