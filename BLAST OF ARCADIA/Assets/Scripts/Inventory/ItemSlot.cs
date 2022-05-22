using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private WeaponScriptableObject _weapon;

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

    protected virtual void OnValidate()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
    }
}
