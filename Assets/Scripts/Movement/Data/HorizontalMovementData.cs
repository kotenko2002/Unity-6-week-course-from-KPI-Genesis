using System;
using UnityEngine;

namespace Assets.Scripts.Movement.Data
{
    [Serializable]
    public class HorizontalMovementData
    {
        [field: SerializeField] public float HorizontalSpeed { get; set; }
        [field: SerializeField] public Direction Direction { get; set; }
    }
}
