using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> woods = new List<GameObject>();
    [SerializeField] private int curDurability;
    [SerializeField] private int repairTime;

    private bool isRepairing;
    private float curRepairTime;

    public bool locked => curDurability > 0;

    public void DisplayUI()
    {
        if (!isRepairing)
            CanvasManager.manager.InteractUI.DisplayUI("Repair", 0.01f);
    }

    public void Interact()
    {
        isRepairing = true;
    }

    private void Start()
    {
        curDurability = 0;

        int i = 0;
        for (; i < curDurability; i++) woods[i].SetActive(true);
        for (; i < woods.Count; i++) woods[i].SetActive(false);
    }

    private void Update()
    {
        Repair();
    }

    private void Repair()
    {
        if (curRepairTime <= 0)
        {
            if (!isRepairing) return;
            if (curDurability >= woods.Count)
            {
                isRepairing = false;
                return;
            }
            if(Vector3.Distance(Player.player.transform.position, transform.position) > Player.player.InteractDist + 1) isRepairing = false;

            curDurability++;

            int i = 0;
            for (; i < curDurability; i++) woods[i].SetActive(true);
            for (; i < woods.Count; i++) woods[i].SetActive(false);

            curRepairTime = repairTime;
        }
        else
        {
            curRepairTime -= Time.deltaTime;
        }
    }
}
