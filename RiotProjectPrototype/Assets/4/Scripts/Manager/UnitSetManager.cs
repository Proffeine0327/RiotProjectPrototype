using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSetManager : MonoBehaviour
{
    public static UnitSetManager manager { get; private set; }

    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject rangePrefeb;
    [SerializeField] private Transform staruigroup;
    [SerializeField] private GameObject startuiprefeb;
    [SerializeField] private GameObject mergeParticle;
    [SerializeField] private List<UnitData> deck = new List<UnitData>();
    [SerializeField] private List<UnitCard> cards = new List<UnitCard>();

    private int money;
    private PlayerUnit[,] units;

    public int Money => money;

    public bool UseCard(int index, Vector2Int yx, Vector3 pos)
    {
        if(money < deck[index].Cost) return false;
        if(units[yx.x, yx.y] != null)
        {
            if(units[yx.x, yx.y].Data.Type != UnitType.trap && units[yx.x, yx.y].Lvl == 1 && units[yx.x, yx.y].Data == deck[index])  
            {
                units[yx.x, yx.y].LevelUp();
                Instantiate(mergeParticle, units[yx.x, yx.y].transform.position + Vector3.up * 0.6f, Quaternion.identity);
                money -= deck[index].Cost;
                return true;
            }
            return false;
        }

        var unit = Instantiate(deck[index].Prefeb, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        var startui = Instantiate(startuiprefeb, staruigroup);
        startui.GetComponent<UnitStarUI>().Init(unit.GetComponent<PlayerUnit>());

        units[yx.x, yx.y] = unit.GetComponent<PlayerUnit>();
        units[yx.x, yx.y].Init(deck[index]);
        money -= deck[index].Cost;
        return true;
    }

    public bool MoveUnit(Vector2Int from, Vector2Int to)
    {
        if(from == to) return false;

        if(units[to.x, to.y] != null)
        {
            if(units[to.x, to.y].Data.Type != UnitType.trap && units[to.x, to.y].Lvl != 5 && units[from.x, from.y].Lvl == units[to.x, to.y].Lvl && units[from.x, from.y].Data == units[to.x, to.y].Data)
            {
                units[to.x, to.y].LevelUp();
                Destroy(units[from.x, from.y].gameObject);
                units[from.x, from.y] = null;
                Instantiate(mergeParticle, units[to.x, to.y].transform.position + Vector3.up * 0.6f, Quaternion.identity);
                return true;
            }
            return false;
        }

        units[to.x, to.y] = units[from.x, from.y];
        units[from.x, from.y] = null;
        return true;
    }

    public void ActiveMenu(bool active)
    {
        bg.GetComponent<Image>().enabled = active;
        foreach (var card in cards) card.GetComponent<Image>().enabled = active;
    }

    private void Awake()
    {
        manager = this;
        money = 10000;
    }

    private void Start()
    {
        for (int i = 0; i < cards.Count; i++) cards[i].Init(i, new Vector2(0, 300 - i * 200), deck[i], deck[i].Cardsprite);
        units = new PlayerUnit[GridGroup.group.XYSize.y, GridGroup.group.XYSize.x];
    }
}