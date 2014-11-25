using System;
using System.ComponentModel;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class ValidationOptions : INotifyPropertyChanged
    {
        public ValidationOptions()
        {
        }

        public ValidationOptions(ValidationOptions o)
        {
            _fractionalDigits = o.FractionalDigits;
            _futureDate = o.FutureDate;
            _integerDigits = o.IntegerDigits;
            _maximumLength = o.MaximumLength;
            _minimumLength = o.MinimumLength;
            _maximumValue = o.MaximumValue;
            _minimumValue = o.MinimumValue;
            _notEmpty = o.NotEmpty;
            _nullable = o.Nullable;
            _pastDate = o.PastDate;
            _regexPattern = o.RegexPattern;
            _validate = o.Validate;
        }

        private long? _maximumLength;
        public long? MaximumLength
        {
            get { return _maximumLength; }
            set
            {
                if (_maximumLength != value)
                {
                    _maximumLength = value;
                    PropertyChanged.RaiseEvent(this, "MaximumLength");
                }
            }
        }

        private int? _minimumLength;
        public int? MinimumLength
        {
            get { return _minimumLength; }
            set
            {
                if (_minimumLength != value)
                {
                    _minimumLength = value;
                    PropertyChanged.RaiseEvent(this, "MinimumLength");
                }
            }
        }

        private bool? _nullable;
        public bool? Nullable
        {
            get { return _nullable; }
            set
            {
                if (_nullable != value)
                {
                    _nullable = value;
                    PropertyChanged.RaiseEvent(this, "Nullable");
                }
            }
        }

        private int? _maximumValue;
        public int? MaximumValue
        {
            get { return _maximumValue; }
            set
            {
                if (_maximumValue != value)
                {
                    _maximumValue = value;
                    PropertyChanged.RaiseEvent(this, "MaximumValue");
                }
            }
        }

        private int? _minimumValue;
        public int? MinimumValue
        {
            get { return _minimumValue; }
            set
            {
                if (_minimumValue != value)
                {
                    _minimumValue = value;
                    PropertyChanged.RaiseEvent(this, "MinimumValue");
                }
            }
        }

        private bool? _notEmpty;
        public bool? NotEmpty
        {
            get { return _notEmpty; }
            set
            {
                if (_notEmpty != value)
                {
                    _notEmpty = value;
                    PropertyChanged.RaiseEvent(this, "NotEmpty");
                }
            }
        }

        private string _regexPattern;
        public string RegexPattern
        {
            get { return _regexPattern; }
            set
            {
                if (_regexPattern != value)
                {
                    _regexPattern = value;
                    PropertyChanged.RaiseEvent(this, "RegexPattern");
                }
            }
        }

        private bool _validate;
        public bool Validate
        {
            get { return _validate; }
            set
            {
                if (_validate != value)
                {
                    _validate = value;
                    PropertyChanged.RaiseEvent(this, "Validate");
                }
            }
        }

        private int? _integerDigits;
        public int? IntegerDigits
        {
            get { return _integerDigits; }
            set
            {
                if (_integerDigits != value)
                {
                    _integerDigits = value;
                    PropertyChanged.RaiseEvent(this, "IntegerDigits");
                }
            }
        }

        private int? _fractionalDigits;
        public int? FractionalDigits
        {
            get { return _fractionalDigits; }
            set
            {
                if (_fractionalDigits != value)
                {
                    _fractionalDigits = value;
                    PropertyChanged.RaiseEvent(this, "FractionalDigits");
                }
            }
        }

        private bool? _pastDate;
        public bool? PastDate
        {
            get { return _pastDate; }
            set
            {
                if (_pastDate != value)
                {
                    _pastDate = value;
                    PropertyChanged.RaiseEvent(this, "PastDate");
                }
            }
        }

        private bool? _futureDate;
        public bool? FutureDate
        {
            get { return _futureDate; }
            set
            {
                if (_futureDate != value)
                {
                    _futureDate = value;
                    PropertyChanged.RaiseEvent(this, "FutureDate");
                }
            }
        }

        private bool? _email;
        public bool? Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged.RaiseEvent(this, "Email");
                }
            }
        }

        public static ApplicableOptions GetApplicableValidationOptionsForType(string type)
        {
            switch (type.ToLower())
            {
                case "system.datetime":
                case "datetime":
                    return ObjectOptions | ApplicableOptions.Date;
                case "string":
                case "system.string":
                    return ObjectOptions | ApplicableOptions.Length | ApplicableOptions.NotEmpty | ApplicableOptions.RegexPattern;
            }
            if (CLRTypes.IsNumericType(type))
                return ObjectOptions | ApplicableOptions.Value | ApplicableOptions.Digits;

            return ObjectOptions | ApplicableOptions.Validate;
        }

        private const ApplicableOptions ObjectOptions = ApplicableOptions.Nullable;

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Flags]
    public enum ApplicableOptions
    {
        None = 0x0,
        Nullable = 0x1,
        Validate = 0x2,
        Length = 0x4,
        Value = 0x8,
        NotEmpty = 0x10,
        RegexPattern = 0x20,
        Digits = 0x40,
        Date = 0x80
    }

    public static class ApplicableOptionsExtension
    {
        public static bool IsSet(this ApplicableOptions options, ApplicableOptions flags)
        {
            return (options & flags) == flags;
        }
    }
}
