using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlbertoController : MonoBehaviour
{
    public AudioSource hitAS;
    public AudioSource attackAS;
    public GameObject healthbar;
    public ManoloController player;
    public float Exp = 0f;
    public int Health = 20;
    public float attackRange = 0.75f;
    public int attackDamage = 5;

    public List<MessageInfo> OnDieMessages = new List<MessageInfo>();

    [SerializeField]
    Animator _ac = null;
    NavMeshAgent _ag;
    int _runHash = Animator.StringToHash("run");
    int _attackHash = Animator.StringToHash("attack");
    int _dieHash = Animator.StringToHash("die");
    int _hitHash = Animator.StringToHash("hit");
    int _idleHash = Animator.StringToHash("idle");

    private void Awake()
    {
        player.Enemies++;
    }

    private void Start()
    {
        _ag = GetComponent<NavMeshAgent>();
        _ag.SetDestination(player.transform.position);
        healthbar.transform.localScale = new Vector3(Health / 100f, .15f, 1f);
    }

    bool _attacking = false;
    bool _moving = false;

    void Update()
    {
        if(!_attacking)
        {
            if(!player.IsAlive)
            {
                _attacking = true;
                _ag.isStopped = true;
                _ac.CrossFade(_idleHash, 0.1f);
                return;
            }
            _ag.SetDestination(player.transform.position);
            if(!_moving)
            {
                _ac.CrossFade(_runHash, 0.1f);
                _moving = true;
            }
            if((player.transform.position - transform.position).sqrMagnitude <= attackRange * attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        _attacking = true;
        _moving = false;
        _ag.SetDestination(transform.position);
        _ag.isStopped = true;
        transform.LookAt(player.transform);
        _ac.CrossFade(_attackHash, 0.1f);
        yield return new WaitForSeconds(0.587f);
        player.GetHit(attackDamage);
        attackAS.Play();
        yield return new WaitForSeconds(0.5f);
        _attacking = false;
        _ag.isStopped = false;
        _ag.SetDestination(player.transform.position);
    }

    IEnumerator Hit()
    {
        _attacking = true;
        _moving = false;
        _ag.isStopped = true;
        _ac.CrossFade(_hitHash, 0.3f);
        yield return new WaitForSeconds(0.6f);
        _attacking = false;
        _ag.isStopped = false;
    }

    public void GetHit(int damage)
    {
        if (Health <= 0) return;

        hitAS.Play();
        Health = Mathf.Max(Health - damage, 0);
        healthbar.transform.localScale = new Vector3(Health / 100f, .15f, 1f);
        if (Health <= 0)
        {
            StopAllCoroutines();
            Health = 0;
            _ac.CrossFade(_dieHash, 0.1f);
            _ag.isStopped = true;
            _attacking = true;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2f);
            player.GetXP(Exp);
            DialogController.Instance.PlaySequence(OnDieMessages);
            player.Enemies--;
            return;
        }
        StartCoroutine(Hit());
    }
}
