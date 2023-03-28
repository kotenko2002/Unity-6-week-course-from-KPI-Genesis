using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InputReader
{
    public class GameUIInputView : MonoBehaviour, IEntityInputSourse
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _jumpButton;

        public float Direction => _joystick.Horizontal;
        public bool Jump { get; private set; }

        public void ResetOneTimeActions()
        {
            Jump= false;
        }

        private void Awake()
        {
            _jumpButton.onClick.AddListener(() => Jump = true);
        }

        private void OnDestroy()
        {
            _jumpButton.onClick.RemoveAllListeners();
        }
    }
}
