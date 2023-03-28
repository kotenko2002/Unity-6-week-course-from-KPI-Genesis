using Assets.Scripts.Core.Services.Updater;
using System;
using UnityEngine;

namespace Assets.Scripts.InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSourse, IDisposable
    {
        public float Direction => Input.GetAxisRaw("Horizontal");
        public bool Jump { get; private set; }

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
        }

        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;

        private void OnUpdate()
        {
            if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow))
            {
                Jump = true;
            }
        }
    }
}
