using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class BuildingPlacer : Subject
    {
        private IBuildingPlacementStrategy _buildingPlacementStrategy;

        public BuildingPlacer(IBuildingPlacementStrategy pBuildingPlacementStrategy)
        {
            _buildingPlacementStrategy = pBuildingPlacementStrategy;
        }

        public UnitView StartPlacement(BuildableData pBuildableData)
        {
            UnitView unitView = _buildingPlacementStrategy.StartPlacement(pBuildableData);
            if (unitView != null)
            {
                NotifyObservers(Messages.BUILDABLE_BUILT, pBuildableData.Buildable);
            }

            return unitView;
        }
    }
}
