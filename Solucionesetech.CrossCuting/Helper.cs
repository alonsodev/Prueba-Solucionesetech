using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Solucionesetech.CrossCutting
{
    public class Helper
    {
        public static int GetCurrentUserId(ClaimsPrincipal user)
        {
            var UserId = "";
            
            if (user != null)
            {
                IEnumerable<Claim> claims = user.Claims;
                // or
                UserId = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            }

            return Int32.Parse(UserId);

          
        }
        public static string GetSolucionesetechClientId(ClaimsPrincipal user)
        {
            var UserId = "";

            if (user != null)
            {
                IEnumerable<Claim> claims = user.Claims;
                // or
                UserId = claims.Where(x => x.Type == "odigei_client_id").FirstOrDefault().Value;
            }

            return UserId;


        }

        public static int[] GetPermissions(ClaimsPrincipal user)
        {
            var Permissions = "";

            if (user != null)
            {
                IEnumerable<Claim> claims = user.Claims;
                // or
                Permissions = claims.Where(x => x.Type == "Permissions").FirstOrDefault().Value;
            }
            if (String.IsNullOrEmpty(Permissions))
                return new List<int>().ToArray();
            return Array.ConvertAll(Permissions.Split(","), int.Parse);          


        }

       
        public static bool HasPermission(ClaimsPrincipal user, int permissionId) {

            var permissions = Helper.GetPermissions(user);
            return permissions.Contains(permissionId);
        }

        public static string Encripta(string Par_Word)
        {
            int Par_ID = 1;
            // string ID, StringCode, StringDecode;
            string resultadoEncriptado, OriginalString;

            string[] Encode = new string[5];
            string[] Decode = new string[5];
            int PosStringCode, PosStringDecode = 1;

            Decode[0] = @"|#$9874%&/()°><12356{ë}+*-áÿCÜéö£ôí¥kÆ¼ªóºñò¬Öú½«»û¦_?¡!¿][0.@aêåd\ìæ¶";
            Encode[0] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[1] = @"|#$9874%&/()°><12356{ë}+*-áÿCÜéö£ôí¥kÆ¼ªóºñò¬Öú½«»û¦_?¡!¿][0.@aêåd\ìæ¶";
            Encode[1] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[2] = @"7ê56{}+*-_?¡!¿][0.@|#è$%b/í¥kÆ¼ªóºñò¬Öú½«áÿCÜéö£ô»û¦()°><12398&4çb\ìæ¶";
            Encode[2] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[3] = @")°><126{@}+*-_?3ê8745ïXc!¿ö£ôí¥kÆ¼ªóºáÿCÜéñò¬Öú½«»û¦]|#$%&/([0.¡9ëc\ìæ¶";
            Encode[3] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[4] = @"+*-_?¡87456{!¿][0.@|#ç$%&dkÆ¼ªóºñò¬Öú½«»û¦áÿCÜéö£ôí¥()ê><1239}/°êh\ìæ¶";
            Encode[4] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";

            resultadoEncriptado = string.Empty;

            for (PosStringDecode = 0; PosStringDecode < Par_Word.Length; PosStringDecode++)
            {
                OriginalString = Par_Word.Substring(PosStringDecode, 1);

                for (PosStringCode = 0; PosStringCode < Encode[Par_ID].Length; PosStringCode++)
                {
                    if (OriginalString == Encode[Par_ID].Substring(PosStringCode, 1))
                    {
                        resultadoEncriptado = resultadoEncriptado + Decode[Par_ID].Substring(PosStringCode, 1);
                    }
                }
            }

            return resultadoEncriptado;
        }

        // Generate a random password of a given length (optional)  
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        // Generate a random number between two numbers    
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase  
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
