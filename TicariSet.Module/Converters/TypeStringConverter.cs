using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;

namespace TicariSet.Module.Converters
{
    public class TypeStringConverter : ValueConverter
    {

        #region Overrides
        public override object ConvertFromStorageType(object stringObjectType)
        {
            return ReflectionHelper.FindType((string)stringObjectType);
        }

        public override object ConvertToStorageType(object objectType)
        {
            if (objectType == null)
            {
                return null;
            }

            return ReflectionHelper.GetType(objectType.ToString()).Name;
        }

        public override Type StorageType => typeof(string);

        #endregion
    }
}