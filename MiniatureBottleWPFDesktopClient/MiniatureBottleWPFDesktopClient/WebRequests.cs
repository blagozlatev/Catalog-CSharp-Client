using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MiniatureBottleWPFDesktopClient
{
    public static class WebRequests
    {
        public static string GetSingle(Uri url, string method, string contentType)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;
            }

            try
            {
                String result;
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                result = reader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static List<string> GetList(Uri url, string method, string contentType)
        {
            List<string> result = new List<string>();
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;
            }

            try
            {
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static string SendBottleOrImage(Uri url, string data, 
            string method, string contentType)
        {
            StreamWriter requestWriter;
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(data);
                }
            }

            try
            {
                String result;
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                result = reader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
