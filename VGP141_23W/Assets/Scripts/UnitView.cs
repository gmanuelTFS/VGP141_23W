using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class UnitView : MonoBehaviourSubject, IObserver
    {
        private void SpawnUnit(BuildableData pBuildableData)
        {
            UnitView unitView = Instantiate(pBuildableData.Prefab, transform.position + transform.forward * transform.localScale.z, Quaternion.LookRotation(transform.forward));
            unitView.name = pBuildableData.PlayerFacingName;
            
            NotifyObservers(Messages.BUILDABLE_BUILT, pBuildableData.Buildable);
        }

        public void OnNotified(string pMessage, params object[] pArgs)
        {
            switch (pMessage)
            {
                case Messages.BUILDABLE_COMPLETE when pArgs[0] is BuildableData buildableData:
                    SpawnUnit(buildableData);
                    break;
            }
        }
    }
}
