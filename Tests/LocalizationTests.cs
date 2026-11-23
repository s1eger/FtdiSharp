using System.Globalization;
using FtdiSharp;

namespace Tests;

public sealed class LocalizationTests
{
    [Theory]
    [InlineData("en-US", "FT_Open failed with FTDI status DEVICE_NOT_FOUND (2).")]
    [InlineData("ru-RU", "Операция FT_Open завершилась с ошибкой FTDI: DEVICE_NOT_FOUND (2).")]
    [InlineData("fr-FR", "FT_Open failed with FTDI status DEVICE_NOT_FOUND (2).")]
    public void Message_UsesCurrentUiCultureWithEnglishFallback(string culture, string expectedMessage)
    {
        var previousCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            var exception = new FtdiException(FtdiStatus.DEVICE_NOT_FOUND, "FT_Open");

            Assert.Equal(expectedMessage, exception.Message);
        }
        finally
        {
            CultureInfo.CurrentUICulture = previousCulture;
        }
    }

    [Fact]
    public void ErrorData_DoesNotDependOnMessageLanguage()
    {
        var previousCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");

            var exception = new FtdiException(FtdiStatus.IO_ERROR, "FT_Read");

            Assert.Equal(FtdiStatus.IO_ERROR, exception.Status);
            Assert.Equal("FT_Read", exception.Operation);
        }
        finally
        {
            CultureInfo.CurrentUICulture = previousCulture;
        }
    }
}
