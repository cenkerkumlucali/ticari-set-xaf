using System;
using System.Linq;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Win.Editors
{
    [PropertyEditor(typeof(Type), "TypeConverterEditor", false)]
    public class TypeConverterEditor : TypePropertyEditor
    {
        private Type _objectType;
        public TypeConverterEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
            _objectType = objectType;
        }

        protected override bool IsSuitableType(Type type)
        {

            if (type != null && _objectType == typeof(GenelTipTanimlari))
            {
                return type.GetProperties().FirstOrDefault(x => x.PropertyType == _objectType) != null;
            }
            return false;
        }
    }
}