using UnityEngine;

public class UISetting : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Init()
    {
        _animator.enabled = true;
        _animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        _animator.enabled = true;
        _animator.SetBool("IsOpen", false);
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && _animator.isActiveAndEnabled)
        {
            Close();
        }
    }
}
