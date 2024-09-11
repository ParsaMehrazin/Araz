using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace Utilities
{
    public static class ControlUnit
    {
        public static byte[] Projectkey { get; set; }

        public static void InitializeProjectKey()
        {
            if (Projectkey == null)
            {
                string password = "565f07c5-0a15-4f79-b89d-86d6909";
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                int iterations = 10000;
                int keySize = 256;
                using (var key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] keyBytes = key.GetBytes(keySize / 8);
                    Projectkey = keyBytes;
                }
            }
        }

        //static View_User_ControlUnit loggedUser { get; set; }
        //public static View_User_ControlUnit LoggedUser
        //{
        //    get
        //    {
        //        return loggedUser;
        //    }
        //    set
        //    {
        //        loggedUser = value;
        //    }
        //}

        //static View_Ashkhas_ControlUnit loggedAshkhas { get; set; }
        //public static View_Ashkhas_ControlUnit LoggedAshkhas
        //{
        //    get
        //    {
        //        return loggedAshkhas;
        //    }
        //    set
        //    {
        //        loggedAshkhas = value;
        //    }
        //}

        static string loggedIP { get; set; }
        public static string LoggedIP
        {
            get
            {
                if (string.IsNullOrEmpty(loggedIP))
                {
                    string MachineName = Dns.GetHostName();
                    return Dns.GetHostByName(MachineName).AddressList[0].ToString();
                }
                else
                    return "";
            }
            set
            {
                loggedIP = value;
            }
        }

        //static View_FinancialYear_ControlUnit financialYear { get; set; }
        //public static View_FinancialYear_ControlUnit FinancialYear
        //{
        //    get
        //    {
        //        return financialYear;
        //    }
        //    set
        //    {
        //        financialYear = value;
        //    }
        //}

        //static View_FinancialYear_ControlUnit activeFinancialYear { get; set; }
        //public static View_FinancialYear_ControlUnit ActiveFinancialYear
        //{
        //    get
        //    {
        //        return activeFinancialYear;
        //    }
        //    set
        //    {
        //        activeFinancialYear = value;
        //    }
        //}

        //static View_Tax_ControlUnit tax { get; set; }
        //public static View_Tax_ControlUnit Tax
        //{
        //    get
        //    {
        //        return tax;
        //    }
        //    set
        //    {
        //        tax = value;
        //    }
        //}

        //static View_Role_Old_ControlUnit role { get; set; }
        //public static View_Role_Old_ControlUnit Role
        //{
        //    get
        //    {
        //        return role;
        //    }
        //    set
        //    {
        //        role = value;
        //    }
        //}

        //static List<View_Role_ControlUnit> employee_Roles { get; set; }
        //public static List<View_Role_ControlUnit> Employee_Roles
        //{
        //    get
        //    {
        //        return employee_Roles;
        //    }
        //    set
        //    {
        //        employee_Roles = value;
        //    }
        //}

        static bool isDeveloperMod { get; set; }
        public static bool IsDeveloperMod
        {
            get
            {
                return isDeveloperMod;
            }
            set
            {
                isDeveloperMod = value;
            }
        }

        static string version { get; set; }
        public static string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

    }
}
