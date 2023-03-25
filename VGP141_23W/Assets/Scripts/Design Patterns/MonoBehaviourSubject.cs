using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class MonoBehaviourSubject : MonoBehaviour
    {
        private readonly HashSet<IObserver> _observers;

        public MonoBehaviourSubject()
        {
            _observers = new HashSet<IObserver>();
        }

        public void AddObserver(IObserver pObserver)
        {
            _observers.Add(pObserver);
        }

        public void RemoveObserver(IObserver pObserver)
        {
            _observers.Remove(pObserver);
        }

        protected void NotifyObservers(string pMessage, params object[] pArgs)
        {
            foreach (IObserver observer in _observers)
            {
                observer.OnNotified(pMessage, pArgs);
            }
        }
    }   
}
