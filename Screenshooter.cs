using UnityEngine;

// Generate a screenshot and save to disk with a random name.

public class Screenshooter : MonoBehaviour
{
    public int minW;
    private int superScaleFactor;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
    public int minCharAmount;
    public int maxCharAmount;

    public string screenShotFolderName = "screenshotSession";
    private string fullPath;
    private string lastName;

    public bool hideCursorAtCenter;

    private void Start()
    {
        superScaleFactor = Mathf.CeilToInt(((float) minW / Screen.width));
        Debug.Log("[Screenshooter] - Screenshooter initialized. Screen width: "+ Screen.width.ToString()+", so Superscale factor will be " + superScaleFactor.ToString());

        fullPath = System.IO.Directory.GetCurrentDirectory() + "/Screenshots/" + screenShotFolderName + "/ ";
        Debug.Log("[Screenshooter] - Screenshoots folder: " + fullPath);

        if (!System.IO.Directory.Exists(fullPath))
        {
            System.IO.Directory.CreateDirectory(fullPath);
            Debug.Log("[Screenshooter] - Screenshot folder created.");
        }

        if (hideCursorAtCenter)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        lastName = "";
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

            ScreenCapture.CaptureScreenshot(fullPath + lastName + ".png", superScaleFactor);
            Debug.Log("[Screenshooter] - Screenshot named " + lastName + " has been saved at " + fullPath);
        }

    }

    public string RandomName(string glyphs)
    {
        string result = "";

        int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string

        for (int i = 0; i < charAmount; i++)
        {
            result += glyphs[Random.Range(0, glyphs.Length)];
        }

        return result;
    }
}
