using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private Image bar; 

    private void Update() 
    {
        bar.fillAmount = Player.player.Hp / Player.player.MaxHp;
    }
}
