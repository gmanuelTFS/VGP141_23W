using System;

namespace VGP141_23W
{
    public class RemainingBuildTimeUpdatedEventArgs : EventArgs
    {
        public readonly float BuildCompletionPercentage;

        public RemainingBuildTimeUpdatedEventArgs(float pBuildCompletionPercentage)
        {
            BuildCompletionPercentage = pBuildCompletionPercentage;
        }
    }   
}
