using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class BuildQueueSubject : Subject
    {
        protected new List<IBuildQueueObserver> _observers;
    }
}
