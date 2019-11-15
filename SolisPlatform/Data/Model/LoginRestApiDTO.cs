using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class LoginRestApiDTO
    {
        public class LoginRestAPiobject
        {
            public string result_msg { get; set; }
            public string req_serial_num { get; set; }
            public Result_Data result_data { get; set; }
            public string result_code { get; set; }
        }

        public class Result_Data
        {
            public int accept_order_num { get; set; }
            public int background_color { get; set; }
            public string countryid { get; set; }
            public string createdate { get; set; }
            public int createuserid { get; set; }
            public int current_order_num { get; set; }
            public string disable_time { get; set; }
            public string email { get; set; }
            public string englishname { get; set; }
            public string err_times { get; set; }
            public object gcj_latitude { get; set; }
            public object gcj_longitude { get; set; }
            public object im_token { get; set; }
            public string isDST { get; set; }
            public int is_agree_gdpr { get; set; }
            public int is_gdpr { get; set; }
            public int is_have_im { get; set; }
            public int is_new_version { get; set; }
            public string is_online { get; set; }
            public int is_open_protocol { get; set; }
            public int is_receive_notice { get; set; }
            public int is_share_position { get; set; }
            public int is_upload_location { get; set; }
            public int is_valid_mobile_email { get; set; }
            public string isdst { get; set; }
            public object jobs { get; set; }
            public string language { get; set; }
            public string loginFristDate { get; set; }
            public string loginLastDate { get; set; }
            public string loginLastIp { get; set; }
            public int loginTimes { get; set; }
            public string login_state { get; set; }
            public object logo { get; set; }
            public object logo_https_url { get; set; }
            public string map_type { get; set; }
            public string min_date { get; set; }
            public string mobile_tel { get; set; }
            public string org_id { get; set; }
            public string org_name { get; set; }
            public string org_timezone { get; set; }
            public int password_is_simple { get; set; }
            public object photo_id { get; set; }
            public object photo_url { get; set; }
            public object[] privileges { get; set; }
            public string role_id { get; set; }
            public string server_tel { get; set; }
            public string service_version { get; set; }
            public string sex { get; set; }
            public string stylename { get; set; }
            public string timeZone { get; set; }
            public string timezone { get; set; }
            public string timezoneid { get; set; }
            public string toggleflag { get; set; }
            public string token { get; set; }
            public int unlock_lave_minute { get; set; }
            public string upload_time { get; set; }
            public string user_account { get; set; }
            public object user_dealer_org_code { get; set; }
            public int user_id { get; set; }
            public string user_level { get; set; }
            public string user_master_org_id { get; set; }
            public string user_master_org_name { get; set; }
            public string user_master_org_time_zone_id { get; set; }
            public string user_master_org_time_zone_name { get; set; }
            public string user_name { get; set; }
            public string[] user_role_id_list { get; set; }
            public string user_tel_nation_code { get; set; }
            public object userauthorURL { get; set; }
            public object[] userauthorbutto { get; set; }
            public string userdesc { get; set; }
            public string userpassword { get; set; }
            public string validflag { get; set; }
            public string voice { get; set; }
            public string welcometext { get; set; }
            public object wgs84_latitude { get; set; }
            public object wgs84_longitude { get; set; }
            public string work_tel { get; set; }
        }
        public class SunGrowLoginModel
        {
            public string Token { get; set; }
            public int UserId { get; set; }
        }
    }
}
