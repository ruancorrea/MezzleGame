using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Image img;
    private bool isEnabled = false;

    public void ClickVewImage() {
        isEnabled = !isEnabled;
        img.gameObject.SetActive (isEnabled);
    }
}
