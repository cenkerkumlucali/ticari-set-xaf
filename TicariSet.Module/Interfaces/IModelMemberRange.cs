using System;
using DevExpress.ExpressApp.Model;

namespace TicariSet.Module.Interfaces
{
    public interface IModelMemberRange : IModelNode
    {
        Decimal MinValue { get; set; }

        Decimal MaxValue { get; set; }
    }
}