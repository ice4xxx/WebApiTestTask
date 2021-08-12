using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

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
