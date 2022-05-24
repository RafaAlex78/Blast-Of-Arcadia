using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ItemSlot : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private WeaponScriptableObject _weapon;
    [SerializeField] private ItemToolTip _tooltip;
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

        _tooltip.ShowToolTip(Weapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideToolTip();
    }
}
