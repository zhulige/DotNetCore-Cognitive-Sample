using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CustomVision_Prediction_Sample
{
    class Program
    {
        static string key;
        static string url;
        static HttpClient client = null;

        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                key = args[0];
                Console.Write("key: " + key);
                url = args[1];
                Console.Write("url: " + url);
            }
            else
            {
                Console.Write("Enter key: ");
                key = Console.ReadLine();
                Console.Write("Enter url: ");
                url = Console.ReadLine();
            }

            Console.Write("Enter image file path: ");

            string imageFilePath = Console.ReadLine();

            List<V_KeyValue> _List = GetImageList(imageFilePath);

            int i = 0;
            while (i < _List.Count)
            {
                MakePredictionRequest(imageFilePath + "/" + _List[i].Value).Wait();
                i++;
                Thread.Sleep(200);
            }

            Console.WriteLine("\n\n\nHit ENTER to exit...");

            Console.ReadLine();
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
        
        static async Task MakePredictionRequest(string imageFilePath)
        {
            try
            {
                if (client == null)
                {
                    client = new HttpClient();

                    // Request headers - replace this example key with your valid subscription key.
                    client.DefaultRequestHeaders.Add("Prediction-Key", key);
                }

                HttpResponseMessage response;

                // Request body. Try this sample with a locally stored image.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(url, content);
                    Hashtable _Hashtable = JsonConvert.DeserializeObject<Hashtable>(await response.Content.ReadAsStringAsync());
                    List<Hashtable> _List = JsonConvert.DeserializeObject<List<Hashtable>>(System.Convert.ToString(_Hashtable["Predictions"]));
                    string str = string.Empty;
                    for (int i = 0; i < _List.Count; i++)
                    {
                        str += "Tag_" + _List[i]["Tag"] + "; " + System.Convert.ToInt32(_List[i]["Probability"]) * 100 + "%; ";
                    }
                    LogHelper.WriteLine(LogLevel.Trace, str, imageFilePath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(LogLevel.Error, ex.Message, imageFilePath);
            }
        }

        /// <summary>
        /// 获取目录内图片列表
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static List<V_KeyValue> GetImageList(string file)
        {
            List<V_KeyValue> _List = new List<V_KeyValue>();

            DirectoryInfo folder = new DirectoryInfo(file);

            foreach (FileInfo _FileInfo in folder.GetFiles("*.png"))
            {
                V_KeyValue _V_KeyValue = new V_KeyValue();
                _V_KeyValue.Key = _FileInfo.Name.Replace(".png", "");
                _V_KeyValue.Value = _FileInfo.Name;
                _List.Add(_V_KeyValue);
            }

            return _List;
        }
    }

    public class V_KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
