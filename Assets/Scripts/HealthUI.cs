using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    private float _visibleTime = 5f;
    private float _lastMadeVisibleTime;
    private Image _heatlhSlider;
    private Transform _camera;
    private Transform _ui;

    public GameObject uiPrefab;
    public Transform healthPoint;

    private void Start()
    {
        _camera = Camera.main.transform;
        foreach (Canvas canva in FindObjectsOfType<Canvas>())
        {
            if (canva.renderMode == RenderMode.WorldSpace)
            {
                _ui = Instantiate(uiPrefab, canva.transform).transform;
                _heatlhSlider = _ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (_ui == null) return;
        _ui.gameObject.SetActive(true);
        _lastMadeVisibleTime = Time.time;
        float healthPercent = (float)currentHealth / maxHealth;
        _heatlhSlider.fillAmount = healthPercent;
        if (currentHealth <= 0)
            Destroy(_ui.gameObject);
    }

    private void LateUpdate()
    {
        if (_ui == null) return;
        _ui.position = healthPoint.position;
        _ui.forward = -_camera.forward;
        if (Time.time - _lastMadeVisibleTime > _visibleTime)
            _ui.gameObject.SetActive(false);
    }
}
