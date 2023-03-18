using System;

namespace VGP141_23W
{
    public static class BuildableExtensionMethods
    {
        public static BuildableType ToBuildableType(this TechTree.Buildable pBuildable)
        {
            switch (pBuildable)
            {
                case TechTree.Buildable.ConstructionYard:
                case TechTree.Buildable.Barracks:
                case TechTree.Buildable.WarFactory:
                case TechTree.Buildable.BattleLab:
                case TechTree.Buildable.OreRefinery:
                    return BuildableType.Building;
                case TechTree.Buildable.GI:
                case TechTree.Buildable.Engineer:
                case TechTree.Buildable.Spy:
                    return BuildableType.Infantry;
                case TechTree.Buildable.GrizzlyTank:
                case TechTree.Buildable.IFV:
                case TechTree.Buildable.Miner:
                    return BuildableType.Vehicle;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pBuildable), pBuildable, null);
            }
        }
    }  
}
