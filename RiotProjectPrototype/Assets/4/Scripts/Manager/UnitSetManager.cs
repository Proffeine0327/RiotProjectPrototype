using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSetManager : MonoBehaviour
{
    public static UnitSetManager manager { get; private set; }

    [SerializeField] private GameObject bg;
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Material roadMaterial;
    [SerializeField] private List<UnitData> deck = new List<UnitData>();
    [SerializeField] private List<UnitCard> cards = new List<UnitCard>();

    private GameObject dragObject;

    public void ActiveMenu(bool active)
    {
        bg.GetComponent<Image>().enabled = active;
        foreach (var card in cards) card.GetComponent<Image>().enabled = active;
    }

    public void DragCard(UnitType type)
    {
        SetGridGroup.group.ActiveGrid(true);
        if (type == UnitType.normal)
        {
            groundMaterial.color = Color.cyan;
            roadMaterial.color = Color.red;
        }
        else
        {
            groundMaterial.color = Color.red;
            roadMaterial.color = Color.cyan;
        }
    }

    public bool UseCard(int index, Vector3 pos)
    {
        Instantiate(deck[index].Prefeb, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        return true;
    }

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        for (int i = 0; i < cards.Count; i++) cards[i].Init(i, new Vector2(0, 300 - i * 200), deck[i]);
    }

    private void Update()
    {
        DraggingUnit();
    }

    private void DraggingUnit()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(bg.transform as RectTransform, Input.mousePosition)) return;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Unit")))
            {
                dragObject = hitinfo.collider.gameObject;
                ActiveMenu(false);
                SetGridGroup.group.ActiveGrid(true);
            }
        }

        if (Input.GetMouseButton(0) && dragObject != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Grid")))
                dragObject.transform.position = new Vector3(hitinfo.collider.transform.position.x, 0, hitinfo.collider.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragObject = null;
            ActiveMenu(true);
            SetGridGroup.group.ActiveGrid(false);
        }
    }
}