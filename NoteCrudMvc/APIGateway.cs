using Newtonsoft.Json;
using NoteCrudMvc.Models;
using System.Net;
using System.Text;

namespace NoteCrudMvc
{
    public class APIGateway
    {
        private string url = "https://localhost:7204/api/Note";
        private HttpClient httpClient=new HttpClient();



        public List<Note> ListNotes()
        {
            List<Note> notes = new List<Note>();
            if(url.Trim().Substring(0,5).ToLower()=="https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Note>>(result);
                    if (datacol != null)
                        notes = datacol;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("hata " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("hata " + ex.Message);

            }
            finally { }
            return notes;
        }



        public Note CreateNote(Note note)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json =JsonConvert.SerializeObject(note);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url,new StringContent(json,Encoding.UTF8,"application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Note>(result);
                    if (data != null)
                        note = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("hata " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("hata " + ex.Message);

            }
            finally { }
            return note;
        }


        public Note GetNote(int Id)
        {
            Note note = new Note();
            url = url + "/" + Id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject <Note> (result);
                    if (data != null)
                        note = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("hata " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("hata " + ex.Message);

            }
            finally { }
            return note;
        }


        public void  UpdateNote(Note note )
        {
          
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            int Id = note.Id;
            url = url + "/" + Id;
            string json = JsonConvert.SerializeObject(note);


            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("hata var " + result);

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("hata var " + ex.Message);

            }
            finally { }
            return;
        }



        public void DleteNote(int Id)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            url = url + "/" + Id;


            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("hata var " + result);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("hata var " + ex.Message);

            }
            finally { }
            return;
        }


        public string Token { get; private set; }

        public void Login(string username, string password)
        {
            string loginUrl = "https://localhost:7204/api/Login";

            var loginData = new { UserName = username, Password = password };
            string json = JsonConvert.SerializeObject(loginData);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(loginUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Token = result;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Giriş yapılamadı: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Giriş yapılamadı: " + ex.Message);
            }
        }

        

        public void Register(string username, string email, string password)
        {
            // Kayıt olacak API endpoint'i
            string registerUrl = "https://localhost:7204/api/Register";

            var registerData = new { UserName = username, Email = email, Password = password };
            string json = JsonConvert.SerializeObject(registerData);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(registerUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Kayıt başarıyla tamamlandı.");
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Kayıt yapılamadı: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt yapılamadı: " + ex.Message);
            }
        }

    }
}




