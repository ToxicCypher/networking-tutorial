public static class SVS
{
    public static bool buttonOnePressed = false;
    public static bool buttonTwoPressed = false;
    public static bool buttonThreePressed = false;
    public static bool buttonFourPressed = false;
    public static bool buttonFivePressed = false;
    public static bool buttonSixPressed = false;
    public static bool buttonSevenPressed = false;

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
        else if (buttonId == 3)
        {
            {
                buttonThreePressed = true;
            }
        }
        else if (buttonId == 4)
        {
            {
                buttonFourPressed = true;
            }
        }
        else if (buttonId == 5)
        {
            {
                buttonFivePressed = true;
            }
        }
        else if (buttonId == 6)
        {
            {
                buttonSixPressed = true;
            }
        }
        else if (buttonId == 7)
        {
            {
                buttonSevenPressed = true;
            }
        }
    }

    public static bool CheckIfDoorOneCanOpen()
    {
        if (buttonOnePressed && buttonTwoPressed)
        {
            return true;
        }
        return false;
    }

    public static bool CheckIfDoorTwoCanOpen()
    {
        if (buttonThreePressed && buttonFourPressed && buttonFivePressed && buttonSixPressed && buttonSevenPressed) 
        {
            return true;
        }
        return false;
    }
}

