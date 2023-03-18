using System;
using System.Collections.Generic;
using UnityEngine;

namespace VGP141_23W
{
    using BNode = DirectedGraph<TechTree.Buildable>.Node;
    public class TechTree : Subject, IObserver
    {
        public enum Buildable
        {
            ConstructionYard,
            Barracks,
            WarFactory,
            BattleLab,
            OreRefinery,
            GI,
            Engineer,
            Spy,
            GrizzlyTank,
            IFV,
            Miner,
        }
        
        public static readonly int BuildableCount = Enum.GetNames(typeof(Buildable)).Length;

        private DirectedGraph<Buildable> dependencies;
        private Dictionary<Buildable, int> buildableCounts;
        
        public TechTree()
        {
            dependencies = new DirectedGraph<Buildable>();

            for (int i = 0; i < BuildableCount; i++)
            {
                dependencies.AddNode((Buildable)i);
            }

            // Buildings
            dependencies.AddEdge(Buildable.ConstructionYard, Buildable.Barracks);
            dependencies.AddEdge(Buildable.ConstructionYard, Buildable.OreRefinery);
            dependencies.AddEdge(Buildable.Barracks, Buildable.WarFactory);
            dependencies.AddEdge(Buildable.WarFactory, Buildable.BattleLab);
            // Infantry
            dependencies.AddEdge(Buildable.Barracks, Buildable.GI);
            dependencies.AddEdge(Buildable.Barracks, Buildable.Engineer);
            dependencies.AddEdge(Buildable.Barracks, Buildable.Spy);
            dependencies.AddEdge(Buildable.BattleLab, Buildable.Spy);
            // Vehicles
            dependencies.AddEdge(Buildable.WarFactory, Buildable.GrizzlyTank);
            dependencies.AddEdge(Buildable.WarFactory, Buildable.IFV);
            dependencies.AddEdge(Buildable.WarFactory, Buildable.Miner);
            dependencies.AddEdge(Buildable.OreRefinery, Buildable.Miner);

            buildableCounts = new Dictionary<Buildable, int>();
            for (int i = 0; i < BuildableCount; i++)
            {
                buildableCounts.Add((Buildable)i, 0);
            }
        }

        public bool BuildBuildable(Buildable buildable)
        {
            BNode node = dependencies.FindNode(buildable);
	
            if (node == null) 
            {
                Debug.LogError($"Cannot build {buildable}. Unknown {nameof(Buildable)}");
                return false;
            }
	
            if (!CheckDependencies(node)) 
            {
                Debug.LogError($"Cannot build {node.Data}. Missing dependencies:");
                PrintMissingDependencies(node);
                return false;
            }

            // add structure
            if (buildableCounts.ContainsKey(buildable))
            {
                ++buildableCounts[buildable];
            }
            else
            {
                buildableCounts.Add(buildable, 1);
            }

            return true;
        }

        public bool DestroyBuildable(Buildable buildable)
        {
            // check if already built
            if (buildableCounts.ContainsKey(buildable)) 
            {
                --buildableCounts[buildable];
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool CheckDependencies(Buildable pBuildable)
        {
            BNode node = dependencies.FindNode(pBuildable);
            
            if (node == null) 
            {
                Debug.LogError($"Cannot build {pBuildable}. Unknown {nameof(Buildable)}");
                return false;
            }
            
            return CheckDependencies(node);
        }

        public bool CheckDependencies(BNode pNode)
        {
            List<BNode> parents = pNode.Incoming;
            foreach (BNode parentNode in parents)
            {
                bool valid = buildableCounts.TryGetValue(parentNode.Data, out int count);
                switch (valid)
                {
                    // look up the parent in built structures
                    case true when count <= 0:
                        return false;
                    case false:
                        Debug.LogError($"Could not find {parentNode.Data} in {nameof(buildableCounts)}. Something is seriously wrong.");
                        break;
                }
            }
            // if we get here, all is good
            return true;
        }
        
        private void PrintMissingDependencies(BNode pNode)
        {
            List<BNode> parents = pNode.Incoming;
            foreach (BNode parentNode in parents)
            {
                bool valid = buildableCounts.TryGetValue(parentNode.Data, out int count);
                switch (valid)
                {
                    // look up the parent in built structures
                    case true when count <= 0:
                        Debug.LogError($"\t{parentNode.Data}");
                        PrintMissingDependencies(parentNode);
                        break;
                    case false:
                        Debug.LogError($"Could not find {parentNode.Data} in {nameof(buildableCounts)}. Something is seriously wrong.");
                        break;
                }
            }
        }

        public void OnNotified(string pMessage, params object[] args)
        {
            if (pMessage != Messages.BUILDABLE_BUILT)
            {
                return;
            }

            if (args[0] is not BuildableData buildableData)
            {
                return;
            }

            if (BuildBuildable(buildableData.Buildable))
            {
                NotifyObservers(Messages.TECH_BUILT, buildableData.Buildable);
            }
        }
    }   
}