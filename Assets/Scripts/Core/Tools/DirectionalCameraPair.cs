using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using System;

[Serializable]
public class DirectionalCameraPair
{
    [SerializeField] private CinemachineVirtualCamera _rightCamera;
    [SerializeField] private CinemachineVirtualCamera _leftCamera;

    private Dictionary<Direction, CinemachineVirtualCamera> _directionCameras;

    public Dictionary<Direction, CinemachineVirtualCamera> DirectionCameras
    {
        get
        {
            if(_directionCameras != null)
            {
                return _directionCameras;
            }

            _directionCameras = new Dictionary<Direction, CinemachineVirtualCamera>
            {
                { Direction.Right, _rightCamera },
                { Direction.Left, _leftCamera }
            };

            return _directionCameras;
        }
    }
}
