using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private GameManager _gm;
    [SerializeField] WeaponScriptableObject _drop;
    [SerializeField] List<Sprite> _sprites;

    public WeaponScriptableObject Drop { get => _drop; set => _drop = value; }

    private void Start()
    {
        _gm = GameManager.instance;

        if(Drop is Sword)
        {
            GetComponent<SpriteRenderer>().sprite=_sprites[0];
        }if(Drop is Pistol)
        {
            GetComponent<SpriteRenderer>().sprite=_sprites[1];
            transform.localScale = new Vector3(0.2f,0.2f,1);
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 10);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gm.CreateInstance(Drop);
            Destroy(gameObject);
            
        }
    }
}
