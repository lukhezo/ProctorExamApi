using System;
using System.Collections.Generic;
using System.Net;
using api_request_helpers.ExtensionMethods;

namespace api_request_helpers
{
    class Program
    {
        private static readonly int add_start_days = 15;
        private static readonly int end_start_days = 19;

        static void Main(string[] args)
        {
            var dictionary = CreateExamQueryString();
            UploadString(dictionary["queryString"]);
        }

        public static void UploadString(string data)
        {
            string baseUrl = "https://america.proctorexam.com";
            string resource = "/api/v3/exams?";

             WebClient client = new WebClient {Encoding = System.Text.Encoding.UTF8};

            // Description
            // This API allows for the CRUD of the described resources and the student_sessions for the exam.
            // API Url
            // The current V3 API is set under / api / v3 of the application domain.
            // Accept header
            // The Accept header must also be set to "application / vnd.procwise.v3".
            // Authentication required
            // Authentication token has to be part of the request. It needs to be passed in the Authorization header.Example: Authorization = "Token token=yourToken"
            client.Headers.Set("Accept", "application/vnd.procwise.v3");
            client.Headers.Set("Authorization", $"Token token={Credential.ApiToken}"); // this must be in the format: "Token token=YOURTOKEN"
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            string result = client.UploadString(baseUrl + resource , data);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static Dictionary<string, string> CreateExamQueryString()
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", "Test " + Guid.NewGuid().GetHashCode()},
                {"use_duration", "false"},
                {"start_time", DateTime.UtcNow.AddDays(add_start_days).ToUnixTime().ToString()},
                {"end_time", DateTime.UtcNow.AddDays(end_start_days).ToUnixTime().ToString()},
                {"type", "record_review"},
                {"for_reviewing", "true"},
                {"mobile_cam", "true"},
                {
                    "restrictions",
                    "[[false,[\"\"]],[false,[\"\"]],[false,[\"\"]],[false,[\"\"]],[false,[\"\"]],[false,[\"\"]],[true,[\"\"]]]"
                }
            };

            return  GenerateParameters.GenerateParamsWithSignature(parameters);
        }
    }
}
