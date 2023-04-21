using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeUI : MonoBehaviour 
{
    [SerializeField] private GameObject bg;
    [SerializeField] private List<UpgradeSlotUI> slots = new List<UpgradeSlotUI>();

    public void DisplayUpgradeUI()
    {
        StartCoroutine(UpgradeUIRoutine());
    }

    private IEnumerator UpgradeUIRoutine()
    {
        Time.timeScale = 0;
        bg.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        List<Vector2> startpos = new List<Vector2>();

        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            startpos.Add((slots[i].transform as RectTransform).anchoredPosition);
            (slots[i].transform as RectTransform).DOAnchorPos(new Vector2(-640 + i * 640, -80), 1f).SetEase(Ease.OutBack).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.25f);
        }

        int selectedIndex = -1;
        yield return new WaitUntil(() => {
            for(int i = 0; i < slots.Count; i++)
            {
                if(slots[i].IsSelected)
                {
                    selectedIndex = i;
                    return true;
                }
                else continue;
            }
            return false;
        });

        Player.player.SetUpgrade(slots[selectedIndex].Upgrade);

        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].IsSelected = false;
            slots[i].gameObject.SetActive(false);
            (slots[i].transform as RectTransform).anchoredPosition = startpos[i];
        }
        bg.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
}