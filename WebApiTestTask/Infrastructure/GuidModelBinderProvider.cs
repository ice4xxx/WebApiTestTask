using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApiTestTask.Infrastructure
{
    /// <summary>
    /// Model Binder provider to bind <see cref="Guid"/>
    /// </summary>
    public class GuidModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Guid) ? new GuidModelBinder() : null;
        }
    }
}
