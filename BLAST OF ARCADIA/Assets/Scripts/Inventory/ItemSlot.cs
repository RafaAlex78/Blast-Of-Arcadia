using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private WeaponScriptableObject _weapon;
    public event Action<WeaponScriptableObject> OnRightClickEvent;
    public WeaponScriptableObject Weapon
    {
        get { return _weapon; }
        set
        {
            _weapon = value;
            if (_weapon == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _weapon.Icon;
                _image.enabled = true;
            }

        }
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
    if (eventData !=null &&eventData.button == PointerEventData.InputButton.Right )
        {
            if(Weapon != null && OnRightClickEvent !=null)
            {
                OnRightClickEvent(Weapon);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
    }
}
