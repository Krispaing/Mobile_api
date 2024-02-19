using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace pos_point_system
{
    public class AddParametersToSwaggerUIFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //if (context.ApiDescription.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
            //{
            //    operation.Parameters ??= new List<OpenApiParameter>();

            //    operation.Parameters.Add(new OpenApiParameter
            //    {
            //        Name = "id",
            //        In = ParameterLocation.Query,
            //        Description = "ID of the item",
            //        Required = true,
            //        Schema = new OpenApiSchema
            //        {
            //            Type = "integer",
            //            Format = "int64"
            //        }
            //    });
            //}

            if (context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(context.ApiDescription.ActionDescriptor.Parameters[0].ParameterType, context.SchemaRepository)
                        }
                    }
                };
            }
        }
    }
}
