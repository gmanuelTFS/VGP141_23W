using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class UnitView : MonoBehaviour, IObserver
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnNotified(string pMessage, params object[] pArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}
