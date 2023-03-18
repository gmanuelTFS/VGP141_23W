using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public interface IObserver
    {
        void OnNotified(string pMessage, params object[] pArgs);
    }
}
