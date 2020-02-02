using UnityEngine;

// Generate a screenshot and save to disk with a random name.

public class Screenshooter : MonoBehaviour
{
    public int minW;
    public int superScaleFactor;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
    public int minCharAmount;
    public int maxCharAmount;

    public string screenShotPath;
    public string lastName;

    public bool hideCursorAtCenter;

    private void Start()
    {
        superScaleFactor = Mathf.CeilToInt(((float) minW / Screen.width));

        Debug.Log("[Screenshooter] - Screenshooter initialized. Screen width: "+ Screen.width.ToString()+", so Superscale factor will be " + superScaleFactor.ToString());

        screenShotPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Unity Screenshots");

        lastName = "";

        if (hideCursorAtCenter)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        /*if (hideCursorAtCenter && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }*/

        if (hideCursorAtCenter && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastName = RandomName(glyphs);

            ScreenCapture.CaptureScreenshot(lastName, superScaleFactor);
            Debug.Log("[Screenshooter] - Screenshot named " + lastName + " has been saved at " + screenShotPath + ".");
        }

    }

    public string RandomName(string glyphs)
    {
        string result = "";

        int charAmount = UnityEngine.Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string

        for (int i = 0; i < charAmount; i++)
        {
            result += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }

        return result;
    }
}
