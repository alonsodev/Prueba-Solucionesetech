using Newtonsoft.Json.Converters;

namespace Solucionesetech.Dtos.Common
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}