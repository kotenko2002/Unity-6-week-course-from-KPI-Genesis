using System;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private List<ParallaxLayer> _layers;
    [SerializeField] private Transform _target;

    private float _previousTagretPosition;

    private void Start()
    {
        _previousTagretPosition = _target.position.x;
    }

    private void LateUpdate()
    {
        float deltaMovement = _previousTagretPosition - _target.position.x;
        foreach (var layer in _layers)
        {
            Vector2 layerPosition = layer.Transform.position;
            layerPosition.x += deltaMovement * layer.Speed;

            layer.Transform.position = layerPosition;
        }

        _previousTagretPosition = _target.position.x;
    }

    [Serializable]
    private class ParallaxLayer
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}
