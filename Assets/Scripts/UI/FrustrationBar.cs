using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrationBar : MonoBehaviour {

    public float percentage = 0.0f;

    public float minFade = 0.075f, maxFade = 0.875f;
    public float minFadePercentage = 0.3f, maxFadePercentage = 0.8f;

<<<<<<< HEAD
    public Image redBar;
=======
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
    private Image thisBar;

	public void Start() {
        this.thisBar = base.GetComponent<Image>();
	}
	
	public void Update() {
<<<<<<< HEAD
        if (this.redBar == null || this.thisBar == null)
=======
        if (this.thisBar == null)
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
        {
            return;
        }
        float a;
<<<<<<< HEAD
        float perc = Mathf.Min(this.percentage / 100.0f, 1.0f);
=======
        float perc = this.percentage / 100.0f;
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
        if (perc <= this.minFade)
        {
            a = this.minFadePercentage;
        }
        else if (perc >= this.maxFade)
        {
            a = this.maxFadePercentage;
        }
        else
        {
            float p2 = (perc - this.minFade) / (this.maxFade - this.minFade);
            a = this.minFadePercentage + (this.maxFadePercentage - this.minFadePercentage) * p2;
        }

<<<<<<< HEAD
        Color color = this.redBar.color;
        color.a = a;
        this.redBar.color = color;

        color = this.thisBar.color;
        color.a = a;
        this.thisBar.color = color;

        this.redBar.transform.localScale = new Vector3(perc, 1.0f, 1.0f);
=======
        Color color = this.thisBar.color;
        color.a = a;
        this.thisBar.color = color;
>>>>>>> f9583fce3646315743dcd176ef96bda44faccc17
	}
}
