using System;
using System.Drawing;
using DevExpress.Xpo.Metadata;

namespace TicariSet.Module.OtherClasses
{
    public class ColorConverter : ValueConverter
    {

        #region Overrides

        public override Type StorageType
        {
            get { return typeof(int); }
        }
        public override object ConvertToStorageType(object value)
        {
            if (!(value is Color)) return null;
            return ((Color)value).ToArgb();
        }
        public override object ConvertFromStorageType(object value)
        {
            if (!(value is int)) return null;
            return Color.FromArgb((int)value);
        }


        #endregion

    }
}