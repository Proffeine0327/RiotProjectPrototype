using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace second
{
    public class InGameCardManager : MonoBehaviour
    {
        public static InGameCardManager manager { get; private set; }

        [SerializeField] private Transform castleTarget;
        [SerializeField] private RectTransform bg;
        [SerializeField] private RectTransform cardParent;
        [SerializeField] private GameObject cardPrefeb;
        [SerializeField] private Material previewMaterial;
        [Header("datas")]
        [SerializeField] private float gage; //max : 10;
        [SerializeField] private float gageChargeTime;
        [SerializeField] private UnitDataTable unitdatas;
        [SerializeField] private List<int> openCardIds = new List<int>();

        private Queue<int> closeCardIds = new Queue<int>();
        private List<CardCell> cards = new List<CardCell>();

        public float Gage => gage;

        private void Awake()
        {
            manager = this;
        }

        private void Start()
        {
            for (int i = 0; i < 10; i++) closeCardIds.Enqueue(i % 2);
            for (int i = 0; i < 5; i++)
            {
                openCardIds.Add(closeCardIds.Dequeue());

                cards.Add(Instantiate(cardPrefeb, cardParent).GetComponent<CardCell>());
                cards[i].Init(bg, unitdatas, previewMaterial);
                cards[i].UpdateState(openCardIds[i], i);
            }
        }

        public bool UseCard(int cardIndex, Vector3 spawnPos)
        {
            if (gage < unitdatas.Units[openCardIds[cardIndex]].Cost) return false;

            var selectCard = cards[cardIndex];
            cards.RemoveAt(cardIndex);
            cards.Add(selectCard);
            selectCard.transform.localPosition = new Vector3(899.333f, -288, 0);

            var useCardId = openCardIds[cardIndex];
            openCardIds.RemoveAt(cardIndex);
            openCardIds.Add(closeCardIds.Dequeue());
            closeCardIds.Enqueue(useCardId);

            gage -= unitdatas.Units[useCardId].Cost;
            var unit = Instantiate(unitdatas.Units[useCardId].Prefeb, spawnPos, Quaternion.identity).GetComponent<Unit>();
            unit.Init(castleTarget, new string[] { "Enemy", "Castle" });

            for (int i = 0; i < 5; i++) cards[i].UpdateState(openCardIds[i], i);

            return true;
        }

        private void Update()
        {
            gage += Time.deltaTime / gageChargeTime;
            gage = Mathf.Clamp(gage, 0, 10);
        }
    }
}