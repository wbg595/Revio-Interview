using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // url doesn't have to be initialized to anything important here.
        private string url = "http://api.wunderground.com/api/1cff1d23f9e63f4e/forecast/q/GA/Atlanta.json";
        private string urlParameters = "?api_key=1cff1d23f9e63f4e";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Reset our images and text boxes before writing to them again.
            Label1.Text = "";
            Label1.ForeColor = System.Drawing.Color.Black;
            Image1.ImageUrl = null;
            Image2.ImageUrl = null;
            Image3.ImageUrl = null;

            // This line forces our textbox input to be comma delimited.
            String[] text = TextBox1.Text.Split(',');

            // Make sure string array text has exactly 2 entries.
            if(text.Length == 2)
            {
                // Create the Json URL from our city and state
                String urlString = text[1] + "/" + text[0] + ".json";
                url = "http://api.wunderground.com/api/1cff1d23f9e63f4e/forecast/q/" + urlString;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(json);

                    // Create lists to hold our data objects of interest.
                    List<String> dates = new List<String>();
                    List<String> conditions = new List<String>();
                    List<String> avgWinds = new List<String>();
                    List<String> images = new List<string>();

                    // Using try/catch because all further errors are caused by invalid city or state names using an incorrect URL.
                    try
                    {
                        List<Forecastday2> forecasts = root.forecast.simpleforecast.forecastday;
                        foreach (Forecastday2 f in forecasts)
                        {
                            // Get our data of interest, format it, place into corresponding lists.
                            String date = String.Format(f.date.month + "/" + f.date.day + "/" + f.date.year);
                            dates.Add(date);
                            conditions.Add(f.conditions);
                            String wind = String.Format("Average Windspeed in mph: " + f.avewind.mph + ", in kph: " + f.avewind.kph
                                + ", direction: " + f.avewind.dir);
                            avgWinds.Add(wind);
                            images.Add(f.icon_url);
                        }

                        // Write our last 3 data strings into the label in descending order.
                        for (int i = 3; i > 0; i--)
                        {
                            Label1.Text = Label1.Text + String.Format(dates[i] + ", " + conditions[i] + "<br />" + avgWinds[i])
                                + "<br />" + "<br />";
                        }

                        // Set our image objects to their corresponding image URL's from the Json.
                        if (images.Count == 4)
                        {
                            Image1.ImageUrl = images[3];
                            Image2.ImageUrl = images[2];
                            Image3.ImageUrl = images[1];
                        }
                        else
                        {
                            Image1.ImageUrl = null;
                            Image2.ImageUrl = null;
                            Image3.ImageUrl = null;
                        }
                    }
                    catch
                    {
                        Label1.Text = "Please enter a valid US City and State";
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    Label1.Text = "Error Retrieving Data";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                Label1.Text = "Please use format 'City, State Abbreviation'";
                Label1.ForeColor = System.Drawing.Color.Red;
            }          
        }
    }

    // Json Classes
    public class Features
    {
        public int forecast { get; set; }
    }

    public class Response
    {
        public string version { get; set; }
        public string termsofService { get; set; }
        public Features features { get; set; }
    }

    public class Forecastday
    {
        public int period { get; set; }
        public string icon { get; set; }
        public string icon_url { get; set; }
        public string title { get; set; }
        public string fcttext { get; set; }
        public string fcttext_metric { get; set; }
        public string pop { get; set; }
    }

    public class TxtForecast
    {
        public string date { get; set; }
        public List<Forecastday> forecastday { get; set; }
    }

    public class Date
    {
        public string epoch { get; set; }
        public string pretty { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int yday { get; set; }
        public int hour { get; set; }
        public string min { get; set; }
        public int sec { get; set; }
        public string isdst { get; set; }
        public string monthname { get; set; }
        public string monthname_short { get; set; }
        public string weekday_short { get; set; }
        public string weekday { get; set; }
        public string ampm { get; set; }
        public string tz_short { get; set; }
        public string tz_long { get; set; }
    }

    public class High
    {
        public string fahrenheit { get; set; }
        public string celsius { get; set; }
    }

    public class Low
    {
        public string fahrenheit { get; set; }
        public string celsius { get; set; }
    }

    public class QpfAllday
    {
        public double @in { get; set; }
        public int mm { get; set; }
    }

    public class QpfDay
    {
        public double? @in { get; set; }
        public int? mm { get; set; }
    }

    public class QpfNight
    {
        public double @in { get; set; }
        public int mm { get; set; }
    }

    public class SnowAllday
    {
        public double @in { get; set; }
        public double cm { get; set; }
    }

    public class SnowDay
    {
        public double? @in { get; set; }
        public double? cm { get; set; }
    }

    public class SnowNight
    {
        public double @in { get; set; }
        public double cm { get; set; }
    }

    public class Maxwind
    {
        public int mph { get; set; }
        public int kph { get; set; }
        public string dir { get; set; }
        public int degrees { get; set; }
    }

    public class Avewind
    {
        public int mph { get; set; }
        public int kph { get; set; }
        public string dir { get; set; }
        public int degrees { get; set; }
    }

    public class Forecastday2
    {
        public Date date { get; set; }
        public int period { get; set; }
        public High high { get; set; }
        public Low low { get; set; }
        public string conditions { get; set; }
        public string icon { get; set; }
        public string icon_url { get; set; }
        public string skyicon { get; set; }
        public int pop { get; set; }
        public QpfAllday qpf_allday { get; set; }
        public QpfDay qpf_day { get; set; }
        public QpfNight qpf_night { get; set; }
        public SnowAllday snow_allday { get; set; }
        public SnowDay snow_day { get; set; }
        public SnowNight snow_night { get; set; }
        public Maxwind maxwind { get; set; }
        public Avewind avewind { get; set; }
        public int avehumidity { get; set; }
        public int maxhumidity { get; set; }
        public int minhumidity { get; set; }
    }

    public class Simpleforecast
    {
        public List<Forecastday2> forecastday { get; set; }
    }

    public class Forecast
    {
        public TxtForecast txt_forecast { get; set; }
        public Simpleforecast simpleforecast { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
        public Forecast forecast { get; set; }
    }
}
