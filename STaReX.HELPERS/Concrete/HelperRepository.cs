using Dapper;
using Microsoft.Extensions.Options;
using STaReX.DB.Abstract;
using STaReX.DB.Dtos;
using STaReX.ENTITY.Abstract;
using STaReX.ENTITY.Models.ConnectionTesting;
using STaReX.HELPERS.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using STaReX.ENTITY.Models.Database;

namespace STaReX.HELPERS.Concrete
{
    public class HelperRepository<T> : IHelperRepository<T> where T : IEntity
    {

        private readonly ProcedureOptions _procedureOptions;
        private readonly IDatabaseRepository<HelpersAPI> _repository;

        public HelperRepository(IDatabaseRepository<HelpersAPI> repository, IOptions<ProcedureOptions> options)
        {
            _repository = repository;
            _procedureOptions = options.Value;
        }

        public async Task<T?> GetAllAsyncFromAPI(string keyword, DynamicParameters parameters)
        {
            using (var httpClient = new HttpClient())
            {

                var url = await GetURL(keyword);
       
                if (parameters != null)
                {
                    foreach (var paramName in parameters.ParameterNames)
                    {
                        var value = parameters.Get<dynamic>(paramName);
                        url.URL = url.URL.Replace("{" + paramName + "}", Convert.ToString(value, CultureInfo.InvariantCulture));
                    }

                }

                var response = await httpClient.GetAsync(url.URL);

                if (response.IsSuccessStatusCode)
                { 
                
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    // Handle error response
                    return default(T);

                }
            }
        }

        public async Task<string> GetContext(string path)
        {
            string content;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs))
            {
                content = await reader.ReadToEndAsync();
            }

            return content;

        }

        public async Task<string> GetContextReplacedByCopy(string path_copy, string path_original)
        {
            string content = await GetContext(path_original);

            string content_copy = System.IO.File.ReadAllText(path_copy);

            if (content_copy.Length > 0)
            {
                content = content.Replace(content_copy, "");
            }

            return content;

        }

        public string GetFileNameFromFolder(string[] files)
        {
            string filename;
            string apply_filename = "";
            string converted_filename;
            string date_format = "yyyy_MM_dd-HH_mm_ss";
            DateTime date = new DateTime(2024, 1, 1);
            DateTime converted_date = new DateTime();

            foreach (var file in files)
            {

                filename = Path.GetFileName(file);
                converted_filename = filename.Replace("Q-Zandronum__", "");
                converted_filename = converted_filename.Replace(".log", "");

                if (converted_filename != "Copy")
                {
                    converted_date = DateTime.ParseExact(converted_filename, date_format, CultureInfo.InvariantCulture);

                    if (converted_date > date)
                    {
                        apply_filename = filename;
                        date = converted_date;
                    }
                }

            }

            return apply_filename;
        }

        public async Task<HelpersAPI> GetURL(string keyword)
        {
            DynamicParameters? parametes = new DynamicParameters();
            parametes.Add("KeyWord", keyword);
            var procedure = _procedureOptions.HelpersAPIProcedure.GET_BY_KEYWORD;
            var result = await _repository.GetByIdAsync(procedure, parametes);
            return result;

        }

        public string Encryption(string guid, string password)
        {
            byte[] key = Convert.FromBase64String(password);

            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream()) {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs)) 
                        {
                            sw.Write(guid);
                        }

                        var iv = aes.IV;
                        var encrypted = ms.ToArray();
                        var result = new byte[iv.Length + encrypted.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

                        return Convert.ToBase64String(result);

                    }
                }
            }
        }

        public string Decryption(string token, string password)
        {
            byte[] fullCipher = Convert.FromBase64String(token);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            byte[] key = Convert.FromBase64String(password);

            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decrtyptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream(cipher)) 
                    {
                        using (var cs = new CryptoStream(ms, decrtyptor, CryptoStreamMode.Read))
                        using (var sr = new StreamReader(cs)) 
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
