using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Responses
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? Error { get; private set; }
        public int StatusCode { get; private set; }
        public string? StackTrace { get; set; }

        private Result(bool isSuccess, T? data, string? error, int statusCode, string? stackTrace = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            StatusCode = statusCode;
            StackTrace = stackTrace;
        }

        public static Result<T> Success(T data) => new(true, data, null, 200);
        public static Result<T> Failure(string error, string? stackTrace, int statusCode = 400) => new(false, default, error, statusCode, stackTrace);
    }
}
