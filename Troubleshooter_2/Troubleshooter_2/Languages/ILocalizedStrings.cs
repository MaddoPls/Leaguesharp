namespace Troubleshooter_2.Languages
{
    using System.Resources;

    using Enumerations;

    internal interface ILocalizedStrings
    {
        Languages Language { get; set; }

        ResourceManager ResourceManager { get; set; }

        void ChangeLanguage(Languages language);
    }
}
