using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VGP141_23W;

public class BuildMenuButton : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _buildCountLabel;
    
    private BuildMenu _buildMenu;
    private BuildableData _buildableData;
    private int _buildCount;

    public TechTree.Buildable Buildable => _buildableData.Buildable;

    public void Initialize(BuildMenu pBuildMenu, BuildableData pBuildableData)
    {
        _buildMenu = pBuildMenu;
        _buildableData = pBuildableData;
        _button.onClick.AddListener(CreateBuildRequest);
        _label.text = pBuildableData.PlayerFacingName;
        
        Refresh();
    }

    private void CreateBuildRequest()
    {
        if (_buildCount == 30)
        {
            return;
        }
        
        _fillImage.fillAmount = 1;
        BuildRequest request = new BuildRequest(_buildableData);
        // register to events
        request.RemainingBuildTimeUpdated += OnRequestRemainingBuildTimeUpdated;
        // gives request to build menu
        _buildMenu.AddBuildRequest(request);
        ++_buildCount;
        
        Refresh();
    }

    private void Refresh()
    {
        _buildCountLabel.text = _buildCount.ToString();
        _buildCountLabel.gameObject.SetActive(_buildCount > 1);
    }

    private void OnRequestRemainingBuildTimeUpdated(object pSender, RemainingBuildTimeUpdatedEventArgs pArgs)
    {
        _fillImage.fillAmount = pArgs.BuildCompletionPercentage;

        if (Mathf.Approximately(pArgs.BuildCompletionPercentage, 0f))
        {
            --_buildCount;
            Refresh();
        }
    }
}
