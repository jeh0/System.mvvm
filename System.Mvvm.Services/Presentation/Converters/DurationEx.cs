namespace System.Mvvm.Converters
{
    public static class DurationEx
    {
        public static string ToShortString(this TimeSpan _duration)
        {
            if (null == _duration) return null;

            if (0 == (_duration.Seconds + _duration.Minutes + _duration.Hours + _duration.Days))
                return null;

            if (_duration < TimeSpan.FromHours(1))
            {
                //string __minutes = 0 == _duration.Minutes ? null : $"{_duration.Minutes:00}:";

                return $"{_duration.Minutes}:{_duration.Seconds:00}";
            }
            else if (_duration > TimeSpan.FromDays(1))
            {
                //string __secunds = 0 == _duration.Seconds ? null : $"{_duration.Seconds:00}";
                //string __hours1 = 0 == _duration.Hours ? null : $"{_duration.Hours:00}";
                //string __minutes1 = 0 == _duration.Minutes ? null : $"{_duration.Minutes:00}";

                return $"{_duration.Days}:{_duration.Hours:00}:{_duration.Minutes:00}:{_duration.Seconds:00}";
            }
            else
            {
                string __hours = 0 == _duration.Hours ? null : $"{_duration.Hours}:";
                string __minutes = 0 == _duration.Minutes && 0 == _duration.Hours ? null : $"{_duration.Minutes:00}:";

                return $"{__hours}{__minutes}{_duration.Seconds:00}";
            }

        }
    }
}
