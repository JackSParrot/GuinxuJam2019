using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ManoloController : MonoBehaviour
{
    public AudioSource slash;
    public Image healthImage;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI wintext;
    public TMPro.TextMeshProUGUI dietext;
    public float speed;
    public int health;
    public GameObject projectilePrefab;
    public Transform wand;
    public GameObject Slash;
    public GameObject Sword;
    public GameObject Shield;
    public Wand[] Wands;
    public int currentWand = 0;
    public GameObject LevelupPopup;
    public GameObject UpgradeWandButton;
    public Image xpImage;
    float xp = 0f;

    public List<MessageInfo> _firstLevelMessages = new List<MessageInfo>();

    public int Enemies = 0;

    public bool IsAlive { get { return _currentHealth > 0; } }

    Rigidbody _rb;
    Animator _ac;
    Camera _mc;
    int _attackHash = Animator.StringToHash("attack");
    int _runHash = Animator.StringToHash("run");
    int _idleHash = Animator.StringToHash("idle");
    int _dieHash = Animator.StringToHash("die");

    bool _magicMode = false;
    bool _moving = false;
    bool _attacking = false;
    int _currentHealth = 0;

    void Start()
    {
        _mc = Camera.main;
        _ac = GetComponent<Animator>();
        ToggleMagicMode();
        _currentHealth = health;
        healthText.text = health.ToString();
        healthImage.fillAmount = 1f;
        foreach(var wand in Wands)
        {
            wand.gameObject.SetActive(false);
        }
        Wands[currentWand].gameObject.SetActive(true);
        LevelupPopup.SetActive(false);
        xpImage.fillAmount = 0f;
        wintext.gameObject.SetActive(false);
        dietext.gameObject.SetActive(false);
        StartCoroutine(Win());
    }

    public void ImproveWand()
    {
        Wands[currentWand].gameObject.SetActive(false);
        currentWand++;
        if (currentWand >= Wands.Length -1) currentWand = Wands.Length - 1;
        if (_magicMode) Wands[currentWand].gameObject.SetActive(true);
    }

    public void Heal()
    {
        GetHit(-(health - _currentHealth));
    }

    public void GetXP(float amount)
    {
        if (!IsAlive) return;

        xp += amount;
        if(xp >= 100f)
        {
            StartCoroutine(ShowLevelUp());
            xp = 0f;
        }
        xpImage.fillAmount = xp / 100f;
    }

    IEnumerator ShowLevelUp()
    {
        UpgradeWandButton.SetActive(currentWand < Wands.Length - 1);
        float target = 1f;
        float time = 0.3f;
        float elapsed = 0f;
        LevelupPopup.SetActive(true);
        while(elapsed < time)
        {
            LevelupPopup.transform.localScale = Vector3.one * target * (elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if(_firstLevelMessages.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            DialogController.Instance.PlaySequence(_firstLevelMessages);
            _firstLevelMessages.Clear();
        }
        Time.timeScale = 0f;
    }

    public void HideLevelUp()
    {
        StartCoroutine(HideLevelUpCT());
    }

    IEnumerator HideLevelUpCT()
    {
        Time.timeScale = 1f;
        float time = 0.3f;
        float elapsed = 0f;
        while (elapsed < time)
        {
            LevelupPopup.transform.localScale = Vector3.one * (1f - (elapsed / time));
            elapsed += Time.deltaTime;
            yield return null;
        }
        LevelupPopup.SetActive(false);
    }

    IEnumerator Win()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            if(Enemies == 0)
            {
                wintext.gameObject.SetActive(true);
                yield break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
        if (LevelupPopup.activeSelf) return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!_attacking)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            if (h != 0f || v != 0f)
            {
                transform.position = transform.position + new Vector3(h, 0f, v).normalized * speed * Time.deltaTime;
                if(!_moving)
                {
                    _ac.CrossFade(_runHash, 0.1f);
                    _moving = true;
                }
            }
            else if (_moving)
            {
                _moving = false;
                _ac.CrossFade(_idleHash, 0.1f);
            }
            var mpx = GameInput.Instance.mousePosition.x;
            var mpy = GameInput.Instance.mousePosition.y;
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = _mc.ScreenPointToRay(GameInput.Instance.mousePosition);
            float pt = 0f;
            plane.Raycast(ray, out pt);
            var mp = ray.GetPoint(pt);
            transform.LookAt(mp);
            if (GameInput.Instance.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }
            if (GameInput.Instance.GetMouseButtonDown(1))
            {
                ToggleMagicMode();
            }
        }
    }

    void ToggleMagicMode()
    {
        _magicMode = !_magicMode;
        Slash.SetActive(!_magicMode);
        Sword.SetActive(!_magicMode);
        Shield.SetActive(!_magicMode);
        Wands[currentWand].gameObject.SetActive(_magicMode);
    }

    IEnumerator Attack()
    {
        _attacking = true;
        _moving = false;
        _ac.CrossFade(_attackHash, 0.1f);
        if(!_magicMode)
        {
            slash.Play();
        }
        yield return new WaitForSeconds(0.3f);
        if (_magicMode)
        {
            Instantiate(Wands[currentWand].Projectile, new Vector3(0f, 1f, 0f) + transform.position, transform.rotation);
        }
        else
        {
            foreach(var enemy in _enemiesToAttack)
            {
                enemy?.GetHit(10);
            }
        }
        yield return new WaitForSeconds(0.36f);
        _attacking = false;
    }

    public void GetHit(int damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        healthText.text = _currentHealth.ToString();
        healthImage.fillAmount = _currentHealth / (float)health;
        if(_currentHealth < 1)
        {
            _ac.CrossFade(_dieHash, 0.1f);
            _attacking = true;
            StartCoroutine(Restart());
        }
    }
    
    IEnumerator Restart()
    {
        dietext.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    List<AlbertoController> _enemiesToAttack = new List<AlbertoController>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            _enemiesToAttack.Add(other.GetComponent<AlbertoController>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            _enemiesToAttack.Remove(other.GetComponent<AlbertoController>());
        }
    }
}
