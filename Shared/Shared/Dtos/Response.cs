﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public string SuccessMessage { get; set; }
        public static Response<T> Success(T data, string successMessage, int statusCode)
        {
            return new Response<T> { Data = data, SuccessMessage = successMessage, StatusCode = statusCode, IsSuccess = true };
        }
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static Response<T> Success(T data,T _data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }


        public static Response<T> Success(string successMessage, int statusCode)
        {
            return new Response<T> { Data = default(T), SuccessMessage = successMessage, StatusCode = statusCode, IsSuccess = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccess = true };
        }
        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccess = false };

        }
    }
}