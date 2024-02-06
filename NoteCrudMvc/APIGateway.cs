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



    }
}
