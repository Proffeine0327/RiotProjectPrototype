using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shutter : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> woods = new List<GameObject>();
    [SerializeField] private int curDurability;
    [SerializeField] private int repairTime;

    private bool isRepairing;
    private float curRepairTime;

    private NavMeshObstacle navObs;

    public bool locked => curDurability > 0;

    public void Damage()
    {
        curDurability--;
        int i = 0;
        for (; i < curDurability; i++) woods[i].SetActive(true);
        for (; i < woods.Count; i++) woods[i].SetActive(false);
    }

    public void DisplayUI()
    {
        if (!isRepairing)
            CanvasManager.manager.InteractUI.DisplayUI(Interact, 0.01f);
    }

    public void Interact()
    {
        isRepairing = true;
    }

    private void Start()
    {
        navObs = GetComponent<NavMeshObstacle>();

        curDurability = woods.Count;

        int i = 0;
        for (; i < curDurability; i++) woods[i].SetActive(true);
        for (; i < woods.Count; i++) woods[i].SetActive(false);
    }

    private void Update()
    {
        navObs.enabled = locked;

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
            if (Vector3.Distance(Player.player.transform.position, transform.position) > Player.player.InteractDist + 1) isRepairing = false;

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
