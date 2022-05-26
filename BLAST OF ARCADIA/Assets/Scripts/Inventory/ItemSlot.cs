using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ItemSlot : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeReference] private WeaponInstance _weaponInstance;
    [SerializeField] private ItemToolTip _tooltip;
    public event Action<WeaponInstance> OnRightClickEvent;
    public WeaponInstance Weapon
    {
        get { return _weaponInstance; }
        set
        {
            _weaponInstance = value;
            if (_weaponInstance == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _weaponInstance.Weapon.Icon;
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
        if(_tooltip == null)
            _tooltip = FindObjectOfType<ItemToolTip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        if(mousePos.x > 1500)
        {
            _tooltip.transform.position = new Vector3(mousePos.x-200,mousePos.y,mousePos.z);

        }
        else
        {
            _tooltip.transform.position = new Vector3(mousePos.x + 200, mousePos.y, mousePos.z);
        }
        Debug.Log(mousePos);

        _tooltip.ShowToolTip(Weapon.Weapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideToolTip();
    }
}
