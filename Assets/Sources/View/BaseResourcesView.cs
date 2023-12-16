using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BaseResourcesView : MonoBehaviour
{
    private const string ResourceUIText = "Resources =";

    [SerializeField] private Base _base;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _base.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _base.ResourceCountChanged -= OnResourceCountChanged;
    }

    private void OnResourceCountChanged(int resourceCount)
    {
        _text.text = $"{ResourceUIText} {resourceCount}";
    }
}
