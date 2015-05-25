using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Net.Http;
using VkNet;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SocNetImg.Controllers
{   

    public class ProfileData
    {
        public string ImgUrl;
        public string PageUrl;
        public ISocialNetworkDataProvider SocialNetwork;
    }

    public interface ISocialNetworkDataProvider
    {
        string ShortName { get; }
        Task<ProfileData> GetProfile(NameValueCollection post);
    };

    public class FacebookDataProvider : ISocialNetworkDataProvider
    {
        public string ShortName { get { return "fb"; } }
        public async Task<ProfileData> GetProfile(NameValueCollection post)
        {
            //var result = await client.GetTaskAsync("graph.facebook.com/search?q=&type=user");
            var client = new FacebookClient(/* CommonClasses.TemporaryTokensStorage.FbAccessToken */);
            var rnd = new System.Random();
            for (int i = 0; i < 1000; i++) 
            {
                int start = rnd.Next(10, 10 * 1000 * 1000);
                try
                {
                    dynamic result = await client.GetTaskAsync((start + i).ToString());
                    var id = result.id; // check id exists
                    bool failGender = new string[] { "male", "female" }.Any(g => post["fb-filter-gender"] == g && result.gender != g);
                    if (failGender)
                    {
                        continue;
                    }
                    dynamic picture = await client.GetTaskAsync((start + i).ToString() + "/picture?redirect=false&type=large");
                    var profileData = new ProfileData { 
                        ImgUrl = picture.data.url as string, 
                        PageUrl = "", 
                        SocialNetwork = this 
                    };
                    return profileData;
                }
                catch (Exception e)
                {
                    continue;
                }
                
            }
            throw new Exception();
            /*var query = post["fb-filter-gender"] == "male" || post["fb-filter-gender"] == "female" ? ("?gender=" + post["fb-filter-gender"]) : "";
            using (HttpClient client = new System.Net.Http.HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://api.randomuser.me/" + query ))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                var entry = (JObject.Parse(result))["results"][0]["user"];
                return new ProfileData { ImgUrl = entry["picture"]["large"].Value<string>(), PageUrl = "", SocialNetwork = this };
            }*/
        }
    }
    
    public class VkDataProvider : ISocialNetworkDataProvider
    {
        public string ShortName { get { return "vk"; } }

        public async Task<ProfileData> GetProfile(NameValueCollection post)
        {
            var rnd = new System.Random();
            for (int i = 0; i < 1000; i++)
            {
                int start = rnd.Next(10, 10 * 1000 * 1000);
                try
                {
                    using (HttpClient client = new System.Net.Http.HttpClient())
                    using (HttpResponseMessage response = await client.GetAsync("https://api.vk.com/method/users.get?user_ids=" 
                        + (start + i).ToString() + "&fields=sex,photo_max_orig,domain"))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var profile = (JObject.Parse(result))["response"][0];
                        if (   post["vk-filter-gender"] == "1" && profile["sex"].Value<int>() != 1
                            || post["vk-filter-gender"] == "2" && profile["sex"].Value<int>() != 2
                            || profile["photo_max_orig"].Value<string>().Contains("vk.com/images/deactivated_400.gif"))
                        {
                            continue;
                        }
                        var profileData = new ProfileData
                        {
                            ImgUrl = profile["photo_max_orig"].Value<string>(),
                            PageUrl = "https://vk.com/" + profile["domain"].Value<string>(),
                            SocialNetwork = this
                        };
                        return profileData;
                    }
                    
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            throw new Exception();
        }

        public async Task<ProfileData> GetProfileWithSearch(NameValueCollection post)
        {
            var rnd = new System.Random();
            var client = new HttpClient();
            var api = new VkApi();
            api.Authorize(CommonClasses.TemporaryTokensStorage.VkAccessToken);
            int itemsCount, pageSize = 20, offset = 200 + rnd.Next(800 - pageSize), qstrRand = rnd.Next('z' - 'a' + 2);
            var gender = post["vk-filter-gender"] == "0" ? ("" + (char)(rnd.Next(2) + (int)'1')) : post["vk-filter-gender"];
            var query = 
                qstrRand == 0 ? "" : ("" + (char)((int)'a' + qstrRand - 1))
                + "&sex=" + gender
                + (post.AllKeys.Contains("vk-filter-age_from") ? "age_from=" + post["vk-filter-age_from"] : "")
                + (post.AllKeys.Contains("vk-filter-age_to") ? "age_to=" + post["vk-filter-age_to"] : "");
            var profiles = api.Users.Search(
                query, 
                out itemsCount, 
                VkNet.Enums.Filters.ProfileFields.Domain | VkNet.Enums.Filters.ProfileFields.PhotoMaxOrig, 
                pageSize, offset);
            var profile = profiles.FirstOrDefault();

            return new ProfileData { ImgUrl = profile.PhotoPreviews.PhotoMax, PageUrl = "https://vk.com/" + profile.Domain, SocialNetwork = this };
        }
    }

    

    public class HomeController : Controller
    {
        static ISocialNetworkDataProvider vk = new VkDataProvider();
        static ISocialNetworkDataProvider fb = new FacebookDataProvider();
        static ISocialNetworkDataProvider[] providers = new ISocialNetworkDataProvider[] { vk, fb };
        static List<ProfileData> profileCache = new List<ProfileData>();
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var us = User;
                var c = 5;
            }
            await CheckAuthorization();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<JsonResult> GetProfile()
        {
            var keys = Request.Form.AllKeys;
            ProfileData profile = null;
            var availNets = providers.Select(p => "filter-" + p.ShortName + "-on").Where(s => Request.Form.AllKeys.Contains(s)).ToArray();
            if (availNets.Length > 0) { 
                ISocialNetworkDataProvider sn = null;
                switch (availNets[new System.Random().Next(availNets.Length)])
                {
                    case "filter-fb-on": sn = fb; break;
                    case "filter-vk-on": sn = vk; break;
                    default: throw new NotImplementedException();
                }
                try 
                {
                    profile = await sn.GetProfile(Request.Form);
                    return Json(profile);
                }
                catch (Exception e)
                {
                    return Json(new { Error = sn.ShortName + "_authorize_failed" });
                }
            }
            else {
                return Json(null);
            }
        }

        private async Task CheckAuthorization() {
            try {
                await vk.GetProfile(Request.Form);
            }
            catch {
                ViewBag.VkNeedAuthMessage = "<span class=\"error\">Нужна авторизация vk.com</span>";
            }
            try
            {
                await fb.GetProfile(Request.Form);
            }
            catch
            {
                ViewBag.FbNeedAuthMessage = "<span class=\"error\">Нужна авторизация Facebook</span>";
            }
        }
    }
}