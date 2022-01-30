using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float attackCooldown = 0.3f;
    public AudioClip clip; 

    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileDespawnTime = 2f;
    [SerializeField] private float minProjectileVelocity = 400;
    [SerializeField] private float maxProjectileVelocity = 500;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _leftSprite;

    private float _timeSinceLastProjectile = 0f;
    private Vector2 _moveDir;
    private Vector2 _faceDir;
    private Vector2 _lastFaceDir;

    // Start is called before the first frame update
    void Start()
    {
        _faceDir = new Vector2(0,0);
        _lastFaceDir = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastProjectile += Time.deltaTime;
        if (_faceDir != Vector2.zero)
        {
            // Projectiles can only be shot after a certain amount of time
            if(_timeSinceLastProjectile > attackCooldown)
            {
                CreateProjectile();
            }
        }
        ChangeAnimation();
    }

    public void LowerCooldown(float cooldown)
    {
        attackCooldown -= cooldown;
    }

    public void AddCooldown(float cooldown)
    {

    }

    public Vector3 GetMoveDir()
    {
        return _moveDir;
    }

    private void ChangeAnimation()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        if (_lastFaceDir == new Vector2(0, 1) || _lastFaceDir == new Vector2(0.707107f, 0.707107f) || _lastFaceDir == new Vector2(-0.707107f, 0.707107f))
        {
            GetComponent<SpriteRenderer>().sprite = _upSprite;
        }
        else if (_lastFaceDir == new Vector2(-1, 0))
        {
            GetComponent<SpriteRenderer>().sprite = _rightSprite;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_lastFaceDir == new Vector2(1, 0))
        {
            GetComponent<SpriteRenderer>().sprite = _rightSprite;
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = _downSprite;
        }
    }

    private void CreateProjectile()
    {
        // Create new projectile
        GameObject newProjectile = Instantiate<GameObject>(projectile);
        newProjectile.transform.position = Player.instance.transform.position;
        newProjectile.GetComponent<Projectile>().SetDamage(Player.instance.projectileDamage);

        //change rotation - (-45 = right, -135 = down, 45 up, 135 left, 0 up right, 90up left, -90down right, -180 down left)
        if (_faceDir == new Vector2(0, 1))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else if (_faceDir == new Vector2(0, -1))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, -135);
        }
        else if (_faceDir == new Vector2(1, 0))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, -45);
        }
        else if (_faceDir == new Vector2(-1, 0))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, 135);
        }
        else if (_faceDir == new Vector2(0.707107f, 0.707107f))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_faceDir == new Vector2(-0.707107f, 0.707107f))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (_faceDir == new Vector2(0.707107f, -0.707107f))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (_faceDir == new Vector2(-0.707107f, -0.707107f))
        {
            newProjectile.transform.eulerAngles = new Vector3(0, 0, -180);
        }
      

        newProjectile.GetComponent<Rigidbody2D>().AddForce(_faceDir * Random.Range(minProjectileVelocity, maxProjectileVelocity));
        _timeSinceLastProjectile = 0f;
        SoundManager.Instance.PlayEffect(clip, transform.position);
        Destroy(newProjectile, projectileDespawnTime);
    }

    // player attack
    public void OnAttack(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        _faceDir = new Vector2(inputVec.x, inputVec.y);
        if (_faceDir != Vector2.zero)
        {
            _lastFaceDir = _faceDir;
        }
    }

    // THis no worky but we lazy
    //public void OnShove()
    //{
    //    Collider2D[] results = new Collider2D[5];
    //    Physics2D.OverlapBox(transform.position, new Vector2(5f,5f), 0f, new ContactFilter2D(), results);
    //    foreach (Collider2D collider in results)
    //    {
    //        if (collider == null)
    //        {
    //            break;
    //        }
    //        if (collider.gameObject.GetComponent<BasicEnemy>())
    //        {
    //            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(_lastFaceDir  * 5000);
    //        }
    //    }
    //}

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        _moveDir = new Vector2(inputVec.x, inputVec.y);
    }


}
