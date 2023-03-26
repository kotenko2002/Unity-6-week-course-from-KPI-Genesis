namespace Assets.Scripts.Player
{
    public interface IEntityInputSourse
    {
        float Direction { get; }
        bool Jump { get; }

        void ResetOneTimeActions();
    }
}
