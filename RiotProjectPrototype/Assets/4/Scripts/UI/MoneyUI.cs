using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour 
{
    private Text txt;

    private void Awake() 
    {
        txt = GetComponent<Text>();    
    }

    private void Update() 
    {
        txt.text = $"{UnitSetManager.manager.Money}$";
    }
}