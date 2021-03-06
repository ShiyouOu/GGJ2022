using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int karma;
    public int maxKarma = 40;
    public bool plrAlive = true;
    //public AudioClip clip; 

    [SerializeField] private int _startHealth;
    [SerializeField] private float _iframeTime = 1f;
    [SerializeField] private int _projectileDamage = 1;
    [SerializeField] private TextMeshProUGUI _karmaText;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private PostProcessVolume _pProcessingVolume;
    [SerializeField] private ColorGrading _colorGrading;
    [SerializeField] private Color _goodColor;
    [SerializeField] private Color _badColor;
    [SerializeField] private AudioClip lightTheme;
    [SerializeField] private AudioClip darkTheme;
    [SerializeField] private AudioClip NeutralTheme;

    private float _timeSinceLastHit = 0f;
    private int _startProjectileDamage;
    private float _startAttackSpeed;
    private Stats _plrStats;

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
        _startProjectileDamage = _projectileDamage;
        _startAttackSpeed = GetComponent<PlayerInput>().attackCooldown;
        _colorGrading = _pProcessingVolume.profile.GetSetting<ColorGrading>();
    }

    private void Start()
    {
        _plrStats = new Stats(_startHealth);
        PlayerHealthbar.instance.UpdateHealth();
        AdjustPostprocessing();
        AdjustMusic();
    }

    private void Update()
    {
        _timeSinceLastHit += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _karmaText.SetText("Karma: " + karma);
    }

    private void AdjustPostprocessing()
    {
        _colorGrading.colorFilter.Override(Color.Lerp(_badColor, _goodColor, (float) (karma + maxKarma) / (maxKarma + maxKarma)));
    }

    private void AdjustMusic()
    {
        float karmaPercent = Mathf.Lerp(0, 1, (float)(karma + maxKarma) / (maxKarma + maxKarma));
        if(karmaPercent >= 0.75) {
            SoundManager.Instance.PlayMusic(lightTheme);
        }else if(karmaPercent <= 0.25)
        {
            SoundManager.Instance.PlayMusic(darkTheme);
        }
        else
        {
            SoundManager.Instance.PlayMusic(NeutralTheme);
        }
    }

    public void RespawnPlayer()
    {
        plrAlive = true;
        _plrStats = new Stats(_startHealth);
        karma = 0;
        _projectileDamage = _startProjectileDamage;
        GetComponent<PlayerInput>().attackCooldown = _startAttackSpeed;
        PlayerHealthbar.instance.UpdateHealth();
        _deathScreen.SetActive(false);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        GetComponent<PlayerInput>().enabled = true;
        GetComponent<Movement>().enabled = true;
        AdjustPostprocessing();
        AdjustMusic();
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
            //SoundManager.Instance.PlayEffect(clip, transform.position);
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

    public void AddKarma(int addKarma)
    {
        karma += addKarma;
        if (karma > maxKarma)
        {
            karma = maxKarma;
        }
        else if (karma < (-1) * maxKarma)
        {
            karma = ((-1) * maxKarma);
        }
        AdjustPostprocessing();
        AdjustMusic();
    }

    public void AddDamage(int dmg)
    {
        _projectileDamage += dmg;
    }
}
