using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace HC.DZWechat.Localization
{
    public static class DZWechatLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(DZWechatConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DZWechatLocalizationConfigurer).GetAssembly(),
                        "HC.DZWechat.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}

