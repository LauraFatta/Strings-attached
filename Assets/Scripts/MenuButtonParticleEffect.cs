using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MenuEffects
{
    public class MenuButtonParticleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Particle Systems")]
        public ParticleSystem clickEffect;
        public ParticleSystem hoverEffect;
        
        [Header("Audio (Optional)")]
        public AudioSource audioSource;
        public AudioClip clickSound;
        public AudioClip hoverSound;
        
        [Header("Button Animation")]
        public float scaleAmount = 0.1f;
        public float animationSpeed = 5f;
        
        private Vector3 originalScale;
        private bool isHovering = false;
        
        void Start()
        {
            originalScale = transform.localScale;
            
            // Get Button component and add listener
            Button button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }
        
        void Update()
        {
            // Smooth scale animation
            Vector3 targetScale = isHovering ? originalScale + Vector3.one * scaleAmount : originalScale;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * animationSpeed);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
            
            // Play hover particle effect
            if (hoverEffect != null)
            {
                hoverEffect.Play();
            }
            
            // Play hover sound
            if (audioSource != null && hoverSound != null)
            {
                audioSource.PlayOneShot(hoverSound);
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
            
            // Stop hover particle effect
            if (hoverEffect != null)
            {
                hoverEffect.Stop();
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // Play click particle effect
            if (clickEffect != null)
            {
                clickEffect.Play();
            }
            
            // Play click sound
            if (audioSource != null && clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
        
        private void OnButtonClick()
        {
            // This method is called by the Button's onClick event
            // You can add additional logic here if needed
        }
        
        // Method to manually trigger the effect (useful for testing)
        public void TriggerClickEffect()
        {
            OnPointerClick(null);
        }
    }
}