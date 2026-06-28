using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Application.Common
{
    public sealed record  Errors(string  code ,  string description, ErrorType Error = ErrorType.Failure)
    {
        public static Errors Failure(string code = "General.Failure", string description = "A general failure has occurred.")
        => new(code, description, ErrorType.Failure);

        public static Errors Validation(string code = "General.Validation", string description = "A validation error has occurred.")
            => new(code, description, ErrorType.Validation);

        public static Errors NotFound(string code = "General.NotFound", string description = "The requested resource was not found.")
            => new(code, description, ErrorType.NotFound);

        public static Errors Conflict(string code = "General.Conflict", string description = "A conflict occurred with the current state.")
            => new(code, description, ErrorType.Conflict);

        public static Errors Unauthorized(string code = "General.Unauthorized", string description = "Access is denied due to lack of authorization.")
            => new(code, description, ErrorType.Unauthorized);

        public static Errors Forbidden(string code = "General.Forbidden", string description = "The operation is forbidden.")
            => new(code, description, ErrorType.Forbidden);

        public static Errors InvalidCredentials(string code = "General.InvalidCredentials", string description = "The provided credentials are invalid.")
            => new(code, description, ErrorType.InvalidCredentials);

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public  enum  ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
        Forbidden = 5,
        InvalidCredentials = 6,

    }

}
