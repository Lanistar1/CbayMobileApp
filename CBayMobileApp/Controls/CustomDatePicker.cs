using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CBayMobileApp.Controls
{
    public class CustomDatePicker : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(CustomDatePicker), defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }

        public static readonly BindableProperty EnterColorProperty = BindableProperty.Create(propertyName: "PlaceholderColor", returnType: typeof(Color), declaringType: typeof(CustomDatePicker), defaultValue: default(Color));
        public Color PlaceholderColor
        {
            get;
            set;
        }
    }
}
