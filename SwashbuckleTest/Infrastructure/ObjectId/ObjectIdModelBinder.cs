using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class ObjectIdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (bindingContext.ModelType != typeof(ObjectId))
                return Task.CompletedTask;

            var valueproviderresult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueproviderresult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueproviderresult);

            if (ObjectId.TryParse(valueproviderresult.FirstValue, out var result))
                bindingContext.Result = ModelBindingResult.Success(result);
            else
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, new InvalidObjectIdException(valueproviderresult.FirstValue), bindingContext.ModelMetadata);

            return Task.CompletedTask;
        }
    }
}
