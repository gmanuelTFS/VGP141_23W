using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VGP141_23W
{
    public class AutoBuildingPlacementStrategy : IBuildingPlacementStrategy
    {
        public UnitView StartPlacement(BuildableData pBuildableData)
        {
            Vector3 spawnPosition = Vector3.zero;

            switch (pBuildableData.Buildable)
            {
                case TechTree.Buildable.ConstructionYard:
                    break;
                case TechTree.Buildable.Barracks:
                    spawnPosition.Set(-5, 0, 5);
                    break;
                case TechTree.Buildable.WarFactory:
                    spawnPosition.Set(-5, 0, -5);
                    break;
                case TechTree.Buildable.BattleLab:
                    spawnPosition.Set(5, 0, 5);
                    break;
                case TechTree.Buildable.OreRefinery:
                    spawnPosition.Set(5, 0, -5);
                    break;
                case TechTree.Buildable.GI:
                case TechTree.Buildable.Engineer:
                case TechTree.Buildable.Spy:
                case TechTree.Buildable.GrizzlyTank:
                case TechTree.Buildable.IFV:
                case TechTree.Buildable.Miner:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // instantiate the prefab
            UnitView buildingView = Object.Instantiate(pBuildableData.Prefab, spawnPosition, Quaternion.LookRotation(Vector3.back));
            buildingView.name = pBuildableData.PlayerFacingName;
            
            return buildingView;
        }
    }
}
