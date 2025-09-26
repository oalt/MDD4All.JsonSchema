using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

namespace MDD4All.JsonSchema.Extensions
{
    public static class JsonSchemaExporterExtensions
    {
        /// <summary>
        /// Generate thd JSON Schema for the given type. 
        /// All exceptions are thrown and should be handled by the user.
        /// </summary>
        /// <param name="dataModelRoot">The root .NET type</param>
        /// <returns>The JSON schema as a JSON string..</returns>
        public static string ToJsonSchema(this Type dataModelRoot)
        {
            try
            {
                JsonSerializerOptions options = JsonSerializerOptions.Default;

                JsonSchemaExporterOptions exporterOptions = new()
                {
                    TreatNullObliviousAsNonNullable = true,

                    TransformSchemaNode = (context, schema) =>
                    {
                        if (schema is JsonObject)
                        {
                            JsonObject jsonObject = (JsonObject)schema;

                            if (jsonObject != null)
                            {
                                jsonObject.Insert(0, "title", context.TypeInfo.Type.Namespace + "." + context.TypeInfo.Type.Name);
                            }
                        }
                        return schema;
                    }

                };

                JsonNode schema = options.GetJsonSchemaAsNode(dataModelRoot, exporterOptions);

                //Console.WriteLine(schema.ToString());

                return schema.ToString();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }


    }
}
