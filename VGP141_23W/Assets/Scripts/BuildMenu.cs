using System;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    public class BuildMenu : MonoBehaviour, IObserver
    {
        public BuildMenuButton BuildMenuButtonPrefab;
        
        private TechTree _techTree;
        private BuildQueue _infantryBuildQueue;
        private BuildQueue _vehicleBuildQueue;
        private BuildQueue _buildingBuildQueue;
        private BuildQueue _defenceBuildingBuildQueue;
        private List<BuildMenuButton> _buildMenuButtons;
        private BuildingPlacer _buildingPlacer;
    
        // Start is called before the first frame update
        void Awake()
        {
            _techTree = new TechTree();
            _techTree.AddObserver(this);
            
            _infantryBuildQueue = new BuildQueue();
            _vehicleBuildQueue = new BuildQueue();
            _buildingBuildQueue = new BuildQueue();
            _defenceBuildingBuildQueue = new BuildQueue();

            _buildMenuButtons = new List<BuildMenuButton>();
            _buildingPlacer = new BuildingPlacer(new AutoBuildingPlacementStrategy());
            
            _buildingPlacer.AddObserver(_techTree);
        }

        private void Start()
        {
            // Populate the menu based on the tech tree
            int buildableCount = TechTree.BuildableCount;
            for (int i = 0; i < buildableCount; i++)
            {
                TechTree.Buildable buildable = (TechTree.Buildable)i;
                BuildMenuButton menuButton = Instantiate(BuildMenuButtonPrefab, transform);
                string buildableName = buildable.ToString();
                menuButton.name = $"{buildableName}BuildMenuButton";
                menuButton.Initialize(this, Resources.Load<BuildableData>($"BuildableData/{buildableName}"));
                
                
                switch (buildable.ToBuildableType())
                {
                    case BuildableType.Building:
                        _buildingBuildQueue.AddObserver(menuButton);
                        break;
                    case BuildableType.DefenceBuilding:
                        _defenceBuildingBuildQueue.AddObserver(menuButton);
                        break;
                    case BuildableType.Infantry:
                        break;
                    case BuildableType.Vehicle:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                _buildingPlacer.AddObserver(menuButton);
                _buildMenuButtons.Add(menuButton);
            }
            
            Refresh();
        }
        
        public void AddBuildRequest(BuildRequest pRequest)
        {
            switch (pRequest.BuildableData.Buildable.ToBuildableType())
            {
                case BuildableType.Infantry:
                    _infantryBuildQueue.AddBuildRequest(pRequest);
                    break;
                case BuildableType.Vehicle:
                    _vehicleBuildQueue.AddBuildRequest(pRequest);
                    break;
                case BuildableType.Building:
                    _buildingBuildQueue.AddBuildRequest(pRequest);
                    break;
                case BuildableType.DefenceBuilding:
                    _defenceBuildingBuildQueue.AddBuildRequest(pRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TriggerBuildProcess(BuildableData pBuildableData)
        {
            /*
             * Start with our automated process
             *      Set positions for buildings that can be built
             *      Prefab to be spawned
             *      Something needs to be a subject so that the BUILDABLE_BUILT message can be sent
             */
            UnitView building = _buildingPlacer.StartPlacement(pBuildableData);
            switch (pBuildableData.Buildable)
            {
                case TechTree.Buildable.ConstructionYard when building != null:
                    break;
                case TechTree.Buildable.Barracks when building != null:
                    _infantryBuildQueue.AddObserver(building);
                    break;
                case TechTree.Buildable.WarFactory when building != null:
                    _vehicleBuildQueue.AddObserver(building);
                    break;
                case TechTree.Buildable.BattleLab:
                case TechTree.Buildable.OreRefinery:
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
        }

        private void Update()
        {
            _infantryBuildQueue.Update(Time.deltaTime);
            _vehicleBuildQueue.Update(Time.deltaTime);
            _buildingBuildQueue.Update(Time.deltaTime);
            _defenceBuildingBuildQueue.Update(Time.deltaTime);
        }

        private void Refresh()
        {
            // Go through all the buttons in the menu and enable those who have their dependencies built
            foreach (BuildMenuButton buildMenuButton in _buildMenuButtons)
            {
                buildMenuButton.gameObject.SetActive(_techTree.CheckDependencies(buildMenuButton.Buildable));
            }
        }

        public void OnNotified(string pMessage, params object[] pArgs)
        {
            switch (pMessage)
            {
                case Messages.TECH_BUILT:
                    Refresh();
                    break;
                case Messages.TECH_DESTROYED:
                    Refresh();
                    break;
            }
        }
    }
}