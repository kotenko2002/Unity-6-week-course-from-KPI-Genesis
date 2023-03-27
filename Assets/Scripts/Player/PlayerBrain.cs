using Assets.Scripts.InputReader;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Player
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSourse> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSourse> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontally(GeteHorizontalDirection());

            if (IsJump)
            {
                _playerEntity.Jump();
            }

            foreach (var inputSource in _inputSources)
            {
                inputSource.ResetOneTimeActions();
            }
        }

        private float GeteHorizontalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if(inputSource.Direction == 0)
                {
                    continue;
                }

                return inputSource.Direction;
            }

            return 0;
        }

        private bool IsJump => _inputSources.Any(sourse => sourse.Jump);
    }
}
