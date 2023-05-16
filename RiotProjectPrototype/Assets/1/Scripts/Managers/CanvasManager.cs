using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace first
{

    public class CanvasManager : MonoBehaviour
    {
        public static CanvasManager manager { get; private set; }

        [SerializeField] private InteractUI interactUI;
        [SerializeField] private JoystickUI joystickUI;
        [SerializeField] private DragUI dragUI;
        [SerializeField] private DecisionUI decisionUI;

        public InteractUI InteractUI => interactUI;
        public JoystickUI JoystickUI => joystickUI;
        public DragUI DragUI => dragUI;
        public DecisionUI DecisionUI => decisionUI;

        private void Awake()
        {
            manager = this;
        }
    }
}
