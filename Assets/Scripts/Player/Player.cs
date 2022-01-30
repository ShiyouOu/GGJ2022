using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum FacingDirection
{
    Up,
    Down,
    Right,
    Left,
}

public class Player : MonoBehaviour
{
    public static Player instance;
    public int karma; 
    public bool plrAlive = true;
    public AudioClip clip; 

    [SerializeField] private int _startHealth;
    [SerializeField] private float _iframeTime = 1f;
    [SerializeField] private int _projectileDamage = 1;
    [SerializeField] private TextMeshProUGUI _karmaText;
    [SerializeField] private GameObject _deathScreen;

    private float _timeSinceLastHit = 0f;
    private int _startProjectileDamage;
    private Stats _plrStats;
    private FacingDirection _plrDirection = FacingDirection.Down;

    public int projectileDamage
    {
        get
        {
            return _projectileDamage;
        }
        set
        {
            return;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _plrStats = new Stats(_startHealth);
        PlayerHealthbar.instance.UpdateHealth();
        _startProjectileDamage = _projectileDamage;
    }

    private void Update()
    {
        _timeSinceLastHit += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _karmaText.SetText("Karma: " + karma);
    }

    public void RespawnPlayer()
    {
        plrAlive = true;
        _plrStats = new Stats(_startHealth);
        karma = 0;
        _projectileDamage = _startProjectileDamage;
        PlayerHealthbar.instance.UpdateHealth();
        _deathScreen.SetActive(false);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        GetComponent<PlayerInput>().enabled = true;
        GetComponent<Movement>().enabled = true;
        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }
    }

    public void TakeDamage(int dmg)
    {
        // Iframe
        if(_timeSinceLastHit < _iframeTime) { return; }

        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);

        // So hp doesn't go under 0
        if (dmg >= _plrStats.currentHP)
        {
            _plrStats.currentHP = 0;
            plrAlive = false;
            _deathScreen.SetActive(true);
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<Movement>().enabled = false;
            foreach (Renderer rend in renderers)
            {
                rend.enabled = false;
            }
        }
        else
        {
            _plrStats.currentHP -= dmg;
            SoundManager.Instance.PlayEffect(clip, transform.position);
        }

        _timeSinceLastHit = 0f;
        // Update the Healthbar UI
        PlayerHealthbar.instance.UpdateHealth();
    }

    public int GetHealth()
    {
        return _plrStats.currentHP;
    }
    public void HealPlayer(int numHealth)
    {
        _plrStats.currentHP += numHealth;
        PlayerHealthbar.instance.UpdateHealth();        
    }
    public void LowerKarma(int lowerKarma)
    {
        karma -= lowerKarma;
    }
    public void AddKarma(int addKarma)
    {
        karma += addKarma;
    }

    public void LowerDamage(int dmg)
    {
        _projectileDamage -= dmg;
    }

    public void AddDamage(int dmg)
    {
        _projectileDamage += dmg;
    }
}
