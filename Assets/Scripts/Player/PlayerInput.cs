using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileDespawnTime = 2f;
    [SerializeField] private float attackCooldown = 0.3f;
    [SerializeField] private float minProjectileVelocity = 400;
    [SerializeField] private float maxProjectileVelocity = 500;

    private float _timeSinceLastProjectile = 0f;
    private Vector2 _moveDir;
    private Vector2 _faceDir;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public Vector3 GetMoveDir()
    {
        return _moveDir;
    }

    private void CreateProjectile()
    {
        // Create new projectile
        GameObject newProjectile = Instantiate<GameObject>(projectile);
        newProjectile.transform.position = Player.instance.transform.position;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(_faceDir * Random.Range(minProjectileVelocity, maxProjectileVelocity));
        _timeSinceLastProjectile = 0f;
        Destroy(newProjectile, projectileDespawnTime);
    }

    public void OnAttack(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        _faceDir = new Vector2(inputVec.x, inputVec.y);
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        _moveDir = new Vector2(inputVec.x, inputVec.y);
    }


}
