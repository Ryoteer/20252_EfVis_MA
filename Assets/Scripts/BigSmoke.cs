using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmoke : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] private float _activationTime = 0.75f;

    [Header("Inputs")]
    [SerializeField] private KeyCode _activationKey = KeyCode.Space;

    [Header("Shader")]
    [SerializeField] private string _scaleFloatName = "_Scale";

    private bool _isHologramActive = false, _isCoroutineActive = false;

    private Animator _animator;
    private Material _material;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0.0f;
        _material = GetComponentInChildren<Renderer>().material;
        _material.SetFloat(_scaleFloatName, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_activationKey) && !_isCoroutineActive)
        {
            StartCoroutine(HologramActivation());
        }
    }

    private IEnumerator HologramActivation()
    {
        _isCoroutineActive = true;

        float t = 0.0f;

        if (!_isHologramActive)
        {
            _animator.speed = 1.0f;
        }

        while(t < 1.0f)
        {
            t += Time.deltaTime / _activationTime;

            if (_isHologramActive)
            {
                _material.SetFloat(_scaleFloatName, Mathf.Lerp(1.0f, 0.0f, t));
            }
            else
            {
                _material.SetFloat(_scaleFloatName, Mathf.Lerp(0.0f, 1.0f, t));
            }

            yield return null;
        }

        if (_isHologramActive)
        {
            _animator.speed = 0.0f;
        }

        _isHologramActive = !_isHologramActive;

        _isCoroutineActive = false;
    }
}
