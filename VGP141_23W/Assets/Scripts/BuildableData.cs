using UnityEngine;
using UnityEngine.Serialization;

namespace VGP141_23W
{
    [CreateAssetMenu(fileName = "NewBuildableData", menuName = "VGP141/Data/Buildable Data")]
    public class BuildableData : ScriptableObject
    {
        [FormerlySerializedAs("Type"),SerializeField] private BuildableType _type;
        [FormerlySerializedAs("BuildTime"),SerializeField] private float _buildTime;
        [FormerlySerializedAs("Prefab"),SerializeField] private UnitView _prefab;
        [SerializeField] private TechTree.Buildable _buildable;
        [SerializeField] private string _playerFacingName;
        
        public float BuildTime => _buildTime;
        public UnitView Prefab => _prefab;
        public TechTree.Buildable Buildable => _buildable;
        public string PlayerFacingName => _playerFacingName;
    }

    public enum BuildableType
    {
        Building,
        DefenceBuilding,
        Infantry,
        Vehicle,
    }
}