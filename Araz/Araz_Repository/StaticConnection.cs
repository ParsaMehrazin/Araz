namespace Repository
{
    public static class StaticConnection
    {
        public static string Connection
        {
            get
            {   
                // ■■■■■■ Develop DB : 
           
                // کانکشن استرینگ دیتابیس تست _ سیستم پارسا ، لپتاپ  
                return "Data Source = ANTONIO ; Initial Catalog = ArazDB ; Persist Security Info = True ;  User ID = sa ; Password = @Parsa1931064210 ; MultipleActiveResultSets = True";
            }
        }

        //public static string DidarToken
        //{
        //    get
        //    {
        //        // ■■■■■■ Didar Token :
        //        // return "524a04b6-d781-4d2e-8e9f-69aa1f45638b";
        //        return "5ooyoutiqrs5wyisrm83io361n0wm65l";
        //    }
        //}

        //public static string CNNDidarApi
        //{
        //    get
        //    {
        //        // ■■■■■■ Didar API :
        //        return "https://app.didar.me/api";
        //    }
        //}

        //public static string SendSMSConnection
        //{
        //    get
        //    {
        //        // ■■■■■■ Didar API :
        //        return "http://192.168.50.85:8008/Api/";
        //    }
        //}

        //Log Database
        //public static string ConnectionLog
        //{
        //    get
        //    {
        //        return "Data Source =192.168.50.46; Initial Catalog =Db_Log;Persist Security Info = True;  User ID = sa; Password =@llDB2020; MultipleActiveResultSets = True";
        //    }
        //}
    }
}
