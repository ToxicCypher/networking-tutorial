public static class SVS
{
    public static bool buttonOnePressed = false;
    public static bool buttonTwoPressed = false;

    public static void UpdateButtonPress(int buttonId)
    {
        if (buttonId == 1)
        {
            buttonOnePressed = true;
        }
        else if (buttonId == 2)
        {
            {
                buttonTwoPressed = true;
            }
        }
    }
}

