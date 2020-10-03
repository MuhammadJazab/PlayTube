
//For Support and help contact us on alidoughous1992@gmail.com
//Full DOC is here >> https://paper.dropbox.com/doc/PlayTube-Mobile-Bundle-szCGKHieAbAIv2VCjBzsR
//Copyright Doughouzlight
//PlayTube V1.0
//*********************************************************

namespace PlayTube
{
    public class Settings
    {
        //Main Settings >>>>>
        //*********************************************************
        public static string WebsiteUrl = "http://test.playtubescript.com";
        public static string Version = "1.0";
        public static string Application_Name = "PlayTube";

        //Main Colors 
        //*********************************************************
        public static string MainColor = "#4ca5ff";

        //ADMOB 
        //*********************************************************
        public static bool Show_ADMOB_On_Timeline = true;
        public static bool Show_ADMOB_On_ItemList = true;
        public static string Ad_Unit_ID = "ca-app-pub-5135691635931982/3190433140";

        //Background Images
        //*********************************************************
        public static string Background_Image_WalkThrough_Page1 = "wp1_bg.jpg";
        public static string Background_Image_WalkThrough_Page2 = "wp2_bg.jpg";
        public static string Background_Image_WalkThrough_Page3 = "wp3_bg.jpg";
        public static string Background_Image_ForgetPasswordPage = "ForgetPassword_bg.jpg";
        public static string Background_Image_RegisterPage = "ForgetPassword_bg.jpg";
        public static string Background_Image_LoginPage = "ForgetPassword_bg.jpg";
        public static string Logo_Image_WelcomePage = "logo.png";
        public static string Logo_Image_LoginPage = "logo.png";

        //Android Display
        //*********************************************************
        public static bool Show_Cutsom_Logo_And_Header_On_the_Top = true;
        public static bool ShowBigFeatured_video = false;

        //Style settings
        ///*********************************************************
        public static bool TurnFullScreenOn = false;
        public static bool YoutubeStyle = true;
        public static bool VimeoStyle = false;

        public static bool DefaultTheme = true;
        public static bool DarkTheme = false;
        public static bool LightTheme = false;


        //Video-Player Control 
        public static string Use_Full_Screen = "1";
        public static string Auto_play = "1";
        public static string Video_height = "300";

        //Bypass Web Erros (OnLogin crach or error set it to true)
        //*********************************************************
        public static bool TurnTrustFailureOn_WebException = false;
        public static bool TurnSecurityProtocolType3072On = false;
        //*********************************************************
    }
}
