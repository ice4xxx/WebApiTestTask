using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace WebApiTestTask.Infrastructure
{
    /// <summary>
    /// Model binder for <see cref="Guid"/>
    /// </summary>
    public class GuidModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var guid = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(guid))
            {
                return Task.CompletedTask;
            }

            try
            {
                Guid guidResult = new Guid(guid);
                bindingContext.Result = ModelBindingResult.Success(guidResult);
            }
            catch
            {
                // ignored
            }

            return Task.CompletedTask;
        }
    }
}
