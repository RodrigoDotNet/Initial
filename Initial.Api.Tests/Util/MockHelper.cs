using Initial.Api.Resources;
using Microsoft.Extensions.Localization;
using Moq;

namespace Initial.Api.Tests.Util
{
    public class MockHelper
    {
        /// <summary>
        /// Método utilizado para criar um mock do Localizer, utilizado no MVC
        /// </summary>
        public static IStringLocalizer<TController> Localizer<TController>()
        {
            var localizer = new Mock<IStringLocalizer<TController>>();

            localizer.Setup(_ => _[It.IsAny<string>()])
                .Returns((string key) =>
                {
                    var value = Messages.ResourceManager.GetString(key);

                    return new LocalizedString(key, value);
                });

            return localizer.Object;
        }

        /// <summary>
        /// Método utilizado para criar um mock do Localizer, utilizado no MVC
        /// </summary>
        public static IStringLocalizer Localizer()
        {
            var localizer = new Mock<IStringLocalizer>();

            localizer.Setup(_ => _[It.IsAny<string>()])
                .Returns((string key) =>
                {
                    var value = Messages.ResourceManager.GetString(key);

                    return new LocalizedString(key, value);
                });

            return localizer.Object;
        }
    }
}