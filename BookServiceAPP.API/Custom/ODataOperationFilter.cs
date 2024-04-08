using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookServiceAPP.API.Custom
{
    public class ODataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if the operation has any parameters
            if (operation.Parameters == null)
                return;

            // Find the ODataQueryOptions parameter
            var odataParam = operation.Parameters.FirstOrDefault(p => p.Name == "options");

            // If ODataQueryOptions parameter exists, remove it and add its properties as separate parameters
            if (odataParam != null && odataParam.Schema.Reference != null && odataParam.Schema.Reference.Id.Contains("ODataQueryOptions"))
            {
                operation.Parameters.Remove(odataParam);

                // Define OData query options
                string[] odataQueryOptions = { "$filter", "$orderby", "$top", "$skip", "$select", "$apply" };

                // Add OData query options as parameters
                foreach (var odataQueryOption in odataQueryOptions)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = odataQueryOption,
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "string" } // You may need to adjust the schema type based on your property types
                    });
                }
            }
        }
    }
}
