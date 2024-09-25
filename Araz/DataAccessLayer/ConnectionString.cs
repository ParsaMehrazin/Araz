namespace Models
{
    public class ConnectionString
    {
        public static string Connection
        {
            get
            {

                // کانکشن استرینگ دیتابیس تستی 
                // بهیچ وجه برای هیچ گونه تستی از این کانکشن استرین استفاده نشود ، تنها و تنها در صورتی که هدف عیب یابی دیتای اصلی باشد .

                // ■■■■■■ Production DB : 
                //return "Data Source = 192.168.50.46 ; Initial Catalog = DbMain ; Persist Security Info = True ;  User ID = sa ; Password = @llDB2020; MultipleActiveResultSets = True";


                // ■■■■■■ Develop DB : 
                // کانکشن استرینگ دیتابیس تست
                //return "Data Source = 192.168.50.46; Initial Catalog = DbMain_Test ; Persist Security Info = True ;  User ID = sa ; Password = @llDB2020 ; MultipleActiveResultSets = True";
                //کانکشن استرینگ دیتابیس تست _ سیستم فلای سپهران
                //return "Data Source = . ; Initial Catalog = DbMain_Test ; Persist Security Info = True ;  User ID = sa ; Password = Emad.B0381)#*! ; MultipleActiveResultSets = True";
                //کانکشن استرینگ دیتابیس تست _ سیستم خانه ، لپتاپ
                //return "Data Source = DESKTOP-78CLETJ ; Initial Catalog = DbMain_Test ; Persist Security Info = True ;  User ID = sa ; Password = Emad.B0381)#*! ; MultipleActiveResultSets = True";
                // کانکشن استرینگ دیتابیس تست _ سیستم پارسا ، لپتاپ  
                return "Data Source = ANTONIO ; Initial Catalog = Araz_DB ; Persist Security Info = True ;  User ID = sa ; Password = @Parsa1931064210 ; MultipleActiveResultSets = True";
            }
        }

        public static string DidarToken
        {
            get
            {
                // ■■■■■■ Didar Token :
                // return "524a04b6-d781-4d2e-8e9f-69aa1f45638b";
                return "5ooyoutiqrs5wyisrm83io361n0wm65l";
            }
        }

        public static string CNNDidarApi
        {
            get
            {
                // ■■■■■■ Didar API :
                return "https://app.didar.me/api";
            }
        }

        public static string SendSMSConnection
        {
            get
            {
                // ■■■■■■ Didar API :
                return "http://192.168.50.85:8008/Api/";
            }
        }

        //Log Database
        public static string ConnectionLog
        {
            get
            {
                return "Data Source =192.168.50.46; Initial Catalog =Db_Log;Persist Security Info = True;  User ID = sa; Password =@llDB2020; MultipleActiveResultSets = True";
            }
        }

    }
}
