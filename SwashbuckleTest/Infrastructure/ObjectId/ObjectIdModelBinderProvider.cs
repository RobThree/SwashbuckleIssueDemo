using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class ObjectIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(ObjectId))
                return new ObjectIdModelBinder();
            return null;
        }
    }
}
