using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrationBar : MonoBehaviour
{

    public float Percentage = 0.0f;

    public float MinFade = 0.075f, MaxFade = 0.875f;
    public float MinFadePercentage = 0.3f, MaxFadePercentage = 0.8f;

    public Image RedBar;
    private Image _thisBar;

    public void Start()
    {
        this._thisBar = base.GetComponent<Image>();
    }

    public void Update()
    {
        if (this.RedBar == null || this._thisBar == null)
        {
            return;
        }
        float a;
        float perc = Mathf.Min(this.Percentage / 100.0f, 1.0f);
        if (perc <= this.MinFade)
        {
            a = this.MinFadePercentage;
        }
        else if (perc >= this.MaxFade)
        {
            a = this.MaxFadePercentage;
        }
        else
        {
            float p2 = (perc - this.MinFade) / (this.MaxFade - this.MinFade);
            a = this.MinFadePercentage + (this.MaxFadePercentage - this.MinFadePercentage) * p2;
        }

        Color color = this.RedBar.color;
        color.a = a;
        this.RedBar.color = color;

        color = this._thisBar.color;
        color.a = a;
        this._thisBar.color = color;

        this.RedBar.transform.localScale = new Vector3(perc, 1.0f, 1.0f);
    }
}
