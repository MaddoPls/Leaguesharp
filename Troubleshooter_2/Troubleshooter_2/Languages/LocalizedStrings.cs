namespace Troubleshooter_2.Languages
{
    using System;
    using System.Resources;

    using Enumerations;

    internal class LocalizedStrings : ILocalizedStrings
    {
        public LocalizedStrings(Languages baseLanguage)
        {
            this.ChangeLanguage(baseLanguage);
        }

        public Languages Language { get; set; }

        public ResourceManager ResourceManager { get; set; }

        public void ChangeLanguage(Languages language)
        {
            switch (this.Language)
            {
                case Languages.Arabic:
                    //this.ResourceManager = ResourceManager
                    break;
                case Languages.Bulgarian:
                    break;
                case Languages.Chinese:
                    break;
                case Languages.Dutch:
                    break;
                case Languages.English:
                    break;
                case Languages.French:
                    break;
                case Languages.German:
                    break;
                case Languages.Greek:
                    break;
                case Languages.Hungarian:
                    break;
                case Languages.Italian:
                    break;
                case Languages.Korean:
                    break;
                case Languages.Latvian:
                    break;
                case Languages.Lithuanian:
                    break;
                case Languages.Polish:
                    break;
                case Languages.Portuguese:
                    break;
                case Languages.Romanian:
                    break;
                case Languages.Russian:
                    break;
                case Languages.Spanish:
                    break;
                case Languages.Swedish:
                    break;
                case Languages.Thai:
                    break;
                case Languages.TraditionalChinese:
                    break;
                case Languages.Turkish:
                    break;
                case Languages.Vietnamese:
                    break;
            }
        }

        /*
         Strings go here
         */
    }
}
