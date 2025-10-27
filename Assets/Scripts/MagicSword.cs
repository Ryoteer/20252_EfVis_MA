using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagicSword : MonoBehaviour
{
    [Header("<color=orange>Renderer</color>")]
    [SerializeField] private float _transitionTime = 0.5f;

    private Animator _animator;
    private Material _material;
    private VisualEffect _vfx;

    private bool _state = false, _isChanging = false;
    private Color _actualColor, _randomColor;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _material = GetComponentInChildren<Renderer>().material;
        _vfx = GetComponentInChildren<VisualEffect>();

        _actualColor = _material.GetColor("_AuraColor");
        _vfx.SetVector4("AuraColor", _actualColor);
    }

    public void SetEvent(string eventName)
    {
        _vfx.SendEvent(eventName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _state = !_state;

            _animator.SetBool("state", _state);
        }
        else if (Input.GetKeyDown(KeyCode.C) && _state && !_isChanging)
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        _isChanging = true;

        _randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);

        float t = 0.0f;

        while (t < 1)
        {
            t += Time.deltaTime / _transitionTime;

            _material.SetColor("_AuraColor", Vector4.Lerp(_actualColor, _randomColor, t));
            _vfx.SetVector4("AuraColor", Vector4.Lerp(_actualColor, _randomColor, t));

            yield return null;
        }

        SetEvent("OnElementChange");

        _actualColor = _randomColor;

        _isChanging = false;
    }

}
