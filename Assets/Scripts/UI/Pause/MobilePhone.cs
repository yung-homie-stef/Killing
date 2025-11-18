using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePhone : MonoBehaviour
{
    private Animator _animator = null;

    private void Awake()
    {
        GameEventsManager.instance.inputEvents.onPauseTogglePressed += AnimateMobilePhone;
        _animator = GetComponent<Animator>();
    }

    private void AnimateMobilePhone(bool flag)
    {
        if (flag)
            _animator.SetFloat("Speed", 1);
        else
            _animator.SetFloat("Speed", -1);

        _animator.SetTrigger("Toggle");
    }
}
