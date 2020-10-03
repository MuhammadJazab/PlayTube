namespace PlayTube.Dependencies
{
    public interface IMethods
    {
        void CopyToClipboard(string text);

        //IMessage =============================
        void LongAlert(string message);
        void ShortAlert(string message);


        //Lang =============================
        void SetLocale();
        string GetCurrent();
    }
}