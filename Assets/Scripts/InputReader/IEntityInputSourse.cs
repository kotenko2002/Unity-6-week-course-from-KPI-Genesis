namespace Assets.Scripts.InputReader
{
    public interface IEntityInputSourse
    {
        float Direction { get; }
        bool Jump { get; }

        void ResetOneTimeActions();
    }
}
