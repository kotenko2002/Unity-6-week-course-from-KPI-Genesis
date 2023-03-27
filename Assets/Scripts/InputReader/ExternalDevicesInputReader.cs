using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSourse
    {
        public float Direction => Input.GetAxisRaw("Horizontal");
        public bool Jump { get; private set; }

        public void OnUpdate()
        {
            if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow))
            {
                Jump = true;
            }
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
        }
    }
}
