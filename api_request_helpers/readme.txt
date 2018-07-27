# ProctorExam API request helpers
Set of scripts with helpers to make requests on the ProctorExam API


# generate_parameters.cs
Create a hash that contains all the parameters that you want to send
Call generate_params_with_signature with the hash as the first argument and the user_secret as the second

Note: timestamp and nonce are automatically generated and added to params:
timestamp:  

DateTime milliseconds = DateTime.UtcNow;
long timestamp = ((DateTimeOffset)milliseconds).ToUnixTimeMilliseconds();

nonce:     
DateTime seconds = DateTime.UtcNow;
long nonce = ((DateTimeOffset)seconds).ToUnixTimeSeconds();
