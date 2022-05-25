using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _valueText;

    public TMP_Text NameText { get => _nameText; set => _nameText = value; }
    public TMP_Text ValueText { get => _valueText; set => _valueText = value; }

    private void OnValidate()
    {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        
        NameText = texts[0];
        ValueText = texts[1];
    }
}
