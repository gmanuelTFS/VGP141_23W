using System;

namespace VGP141_23W
{
    public class BuildRequest
    {
        // what we want to build
        public readonly BuildableData BuildableData;
        // remaining time for this build request
        public float RemainingBuildTime { get; private set; }
        public EventHandler<RemainingBuildTimeUpdatedEventArgs> RemainingBuildTimeUpdated;

        public BuildRequest(BuildableData pBuildableData)
        {
            BuildableData = pBuildableData;
            RemainingBuildTime = BuildableData.BuildTime;
        }

        public void ModifyRemainingTime(float pMod)
        {
            RemainingBuildTime += pMod;

            if (RemainingBuildTime < 0)
            {
                RemainingBuildTime = 0;
            }
            
            RemainingBuildTimeUpdated?.Invoke(this, new RemainingBuildTimeUpdatedEventArgs(RemainingBuildTime / BuildableData.BuildTime));
        }
    }
}
