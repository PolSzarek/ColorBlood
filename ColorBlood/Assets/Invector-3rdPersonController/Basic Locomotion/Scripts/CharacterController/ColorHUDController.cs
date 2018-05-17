using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
namespace Invector.vCharacterController
{
    public class ColorHUDController : MonoBehaviour
    {
        #region General Variables

        #region Health/Stamina Variables
        [Header("Health/Stamina")]
        public List<Light> lights;

        public ParticleSystem energy;
        public float duration = 1.0f;
        public float intensity = 5.0f;
        public float range = 10.0f;
        private float maxRange = 30.0f;
        private float lastHealth;
        private float lastStamina;
        //public Slider staminaSlider;
        [Header("DamageHUD")]
        public Image damageImage;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        [HideInInspector] public bool damaged;
        #endregion

        #region Debug Info Variables
        [Header("Debug Window")]
        public GameObject debugPanel;
        [HideInInspector]
        public Text debugText;
        #endregion

        #region Change Input Text Variables
        [Header("Text with FadeIn/Out")]
        public Text fadeText;
        private float textDuration, fadeDuration, durationTimer, timer;
        private Color startColor, endColor;
        private bool fade;
        #endregion

        #endregion

        private static ColorHUDController _instance;
        public static ColorHUDController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<ColorHUDController>();
                    //Tell unity not to destroy this object when loading a new scene
                    //DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        void Start()
        {
            InitFadeText();
            if (debugPanel != null)
                debugText = debugPanel.GetComponentInChildren<Text>();
        }

        public void Init(vThirdPersonController cc)
        {
            cc.onDead.AddListener(OnDead);
            cc.onReceiveDamage.AddListener(EnableDamageSprite);
            damageImage.color = new Color(0f, 0f, 0f, 0f);
            
            foreach(Light light in lights) {
                setIntensity(light, intensity);
                setRange(light, duration, range);
            }
            lastHealth = cc.maxHealth;
            lastStamina = cc.maxStamina;
        }

        private void OnDead(GameObject arg0)
        {
            
            ShowText("You are Dead!");
        }

        public virtual void UpdateHUD(vThirdPersonController cc)
        {
            UpdateDebugWindow(cc);
            UpdateLights(cc);
            ShowDamageSprite();
            FadeEffect();
        }

        public void ShowText(string message, float textTime = 2f, float fadeTime = 0.5f)
        {
            if (fadeText != null && !fade)
            {
                fadeText.text = message;
                textDuration = textTime;
                fadeDuration = fadeTime;
                durationTimer = 0f;
                timer = 0f;
                fade = true;
            }
            else if (fadeText != null)
            {
                fadeText.text += "\n" + message;
                textDuration = textTime;
                fadeDuration = fadeTime;
                durationTimer = 0f;
                timer = 0f;
            }
        }

        public void ShowText(string message)
        {
            if (fadeText != null && !fade)
            {
                fadeText.text = message;
                textDuration = 2f;
                fadeDuration = 0.5f;
                durationTimer = 0f;
                timer = 0f;
                fade = true;
            }
            else if (fadeText != null)
            {
                fadeText.text += "\n" + message;
                textDuration = 2f;
                fadeDuration = 0.5f;
                durationTimer = 0f;
                timer = 0f;
            }
        }

        void UpdateLights(vThirdPersonController cc)
        {
            if (cc.currentHealth != lastHealth) {
                lastHealth = cc.currentHealth;
                playWithRange(cc);
                //playWithIntensity(cc);
            }

            if (cc.currentStamina != lastStamina) {
                lastStamina = cc.currentStamina;
                sizeParticle(cc);
            }            
        }

        private void playWithRange(vThirdPersonController cc) {
            float value = maxRange - cc.currentHealth / cc.maxHealth * maxRange;
            foreach (Light light in lights) {
                setRange(light, duration, range + value);                        
            }
        }

        private void playWithIntensity(vThirdPersonController cc) {
            float value = cc.currentHealth / cc.maxHealth * intensity;
            foreach (Light light in lights) {
                setIntensity(light, value);                        
            }
        }

        private void sizeParticle(vThirdPersonController cc) {
            float value = cc.currentStamina / cc.maxStamina;

            energy.transform.localScale = new Vector3(value, value, 1);
        }

        public void ShowDamageSprite()
        {
            if (damaged)
            {
                damaged = false;
                if (damageImage != null)
                    damageImage.color = flashColour;
            }
            else if (damageImage != null)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        public void EnableDamageSprite(vDamage damage)
        {
            if (damageImage != null)
                damageImage.enabled = true;
            damaged = true;
        }

        void UpdateDebugWindow(vThirdPersonController cc)
        {
            if (cc.debugWindow)
            {
                if (debugPanel != null && !debugPanel.activeSelf)
                    debugPanel.SetActive(true);
                if (debugText)
                    debugText.text = cc.DebugInfo();
            }
            else
            {
                if (debugPanel != null && debugPanel.activeSelf)
                    debugPanel.SetActive(false);
            }
        }

        void InitFadeText()
        {
            if (fadeText != null)
            {
                fadeText.verticalOverflow = VerticalWrapMode.Overflow;
                startColor = fadeText.color;
                endColor.a = 0f;
                fadeText.color = endColor;
            }
            else
                Debug.Log("Please assign a Text object on the field Fade Text");
        }

        void FadeEffect()
        {
            if (fadeText != null)
            {
                if (fade)
                {
                    fadeText.color = Color.Lerp(endColor, startColor, timer);

                    if (timer < 1)
                        timer += Time.deltaTime / fadeDuration;

                    if (fadeText.color.a >= 1)
                    {
                        fade = false;
                        timer = 0f;
                    }
                }
                else
                {
                    if (fadeText.color.a >= 1)
                        durationTimer += Time.deltaTime;

                    if (durationTimer >= textDuration)
                    {
                        fadeText.color = Color.Lerp(startColor, endColor, timer);
                        if (timer < 1)
                            timer += Time.deltaTime / fadeDuration;
                    }
                }
            }
        }

        private void setIntensity(Light lt, float value) {
            lt.intensity = value;
        }

        private void setRange(Light lt, float duration, float range) {
            float amplitude = Mathf.PingPong(Time.time, duration);
            amplitude = amplitude / duration * 0.5F + 0.5F;
            lt.range = range * amplitude;
        }
    }
}