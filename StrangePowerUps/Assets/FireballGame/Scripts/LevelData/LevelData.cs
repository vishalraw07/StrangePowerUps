public static class LevelData {

    public static string MAIN_MENU = "Assets/FireballGame/Scenes/MainMenu/MainMenu.unity";
    public static string THANKS_MENU = "Assets/FireballGame/Scenes/MainMenu/ThanksMenu.unity";

    public static string LEVEL1 = "Assets/FireballGame/Scenes/Levels/Level1.unity";
    public static string LEVEL2 = "Assets/FireballGame/Scenes/Levels/Level2.unity";
    public static string LEVEL3 = "Assets/FireballGame/Scenes/Levels/Level3.unity";
    public static string LEVEL4 = "Assets/FireballGame/Scenes/Levels/Level4.unity";
    public static string LEVEL5 = "Assets/FireballGame/Scenes/Levels/Level5.unity";

    public static string GetScenePathAt(int i) {
        switch (i) {
            case 0:
                return MAIN_MENU;

            case 1:
                return LEVEL1;

            case 2:
                return LEVEL2;

            case 3:
                return LEVEL3;

            case 4:
                return LEVEL4;

            case 5:
                return LEVEL5;

            case 99:
                return THANKS_MENU;

            default:
                return MAIN_MENU;
        }
    }

}
