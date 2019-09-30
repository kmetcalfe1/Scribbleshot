using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;
    //this public property masks the field _type and takes action wehn it is set
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    void Awake()
    {
        //test to se whether this has passed off screen every 2 seconds
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    public void SetType(WeaponType eType)
    {
        //set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp || bndCheck.offRight)
        {
            Destroy(gameObject);
        }
    }
}