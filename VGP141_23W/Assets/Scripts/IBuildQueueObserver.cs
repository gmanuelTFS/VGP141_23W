namespace VGP141_23W
{
    public interface IBuildQueueObserver : IObserver
    {
        void OnNotified(BuildableData pBuildableData);
    }
}
