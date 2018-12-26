using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace hc.dzwechat.Localization
{
    public static class dzwechatLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(dzwechatConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(dzwechatLocalizationConfigurer).GetAssembly(),
                        "hc.dzwechat.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
