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
    public event Action<WeaponInstance> OnRightClickEvent2;
    private GameManager _gm;
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

    private void Start()
    {
        _gm = GameManager.instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_gm.ShopOpen==false)
        {

            if (eventData !=null &&eventData.button == PointerEventData.InputButton.Right )
            {

                if(Weapon != null && OnRightClickEvent !=null)
                {
                    OnRightClickEvent(Weapon);
                }
            }
            return;
        }
        else
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
               

                if (Weapon != null && OnRightClickEvent2 != null)
                {
                    OnRightClickEvent2(Weapon);
                }
            }
            return ;
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

        _tooltip.ShowToolTip(Weapon.Weapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideToolTip();
    }
}
