using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtConstraint : MonoBehaviour
{
    [Header("Aims")]
    public MultiAimConstraint _headAim;
    public MultiAimConstraint _chestAim;

    [Header("Targets")]
    [SerializeField] public Transform _lookAtTarget;
    [SerializeField] public FirstPersonController _followTarget;

    public bool _isLooking = true;
    private Vector3 _originalFollowTargetPosition = Vector3.zero;

    private void Start()
    {
        _originalFollowTargetPosition = _lookAtTarget.position;
    }


    // Update is called once per frame
    void Update()
    {
        //// temp
        float distanceFromTargetToNPC = Vector3.Distance(transform.position, _followTarget.transform.position);

        if (distanceFromTargetToNPC < 4.0f)
            _isLooking = true;
        else
            _isLooking = false;
        ////

        if (_followTarget != null)
        {
            if (_isLooking)
            _lookAtTarget.position = Vector3.Lerp(_lookAtTarget.transform.position, _followTarget.transform.position, Time.deltaTime * 2.0f);
            else
            _lookAtTarget.position = Vector3.Lerp(_lookAtTarget.transform.position, _originalFollowTargetPosition, Time.deltaTime * 2.0f);
        }

    }

}
