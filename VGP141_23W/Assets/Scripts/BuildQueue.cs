using System;
using UnityEngine;

namespace VGP141_23W
{
    public class BuildQueue : Subject
    {
        private Queue<BuildRequest> _queue;

        public BuildQueue()
        {
            _queue = new Queue<BuildRequest>();
        }

        public void Update(float pDeltaTime)
        {
            // if there is something in the queue
            if (_queue.Size() <= 0)
            {
                return;
            }

            // grab the first thing
            BuildRequest request = _queue.Peek();
            // subtract delta time from remaining time
            request.ModifyRemainingTime(-pDeltaTime);

            if (request.RemainingBuildTime != 0)
            {
                return;
            }

            // dequeue request
            _queue.Dequeue();
            // spawn unit
            // TODO: Change this to be a notify observers
            /*
             * BuildQueue is a subject
             * In-game buildings will observe
             * TechTree will observe
             * Possibly the BuildMenu will observe (it doesn't need to as it is already tightly coupled to BuildQueue
             */
            SpawnUnit(request.BuildableData);

        }

        private void SpawnUnit(BuildableData pBuildableData)
        {
            NotifyObservers(Messages.BUILDABLE_COMPLETE, pBuildableData);
        }

        public void AddBuildRequest(BuildRequest buildRequest)
        {
            _queue.Enqueue(buildRequest);
        }
    }
}
