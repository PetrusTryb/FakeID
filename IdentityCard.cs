using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FakeIDGenerator
{
    class IdentityCard
    {
        private string surname;
        private string givenname;
        private string nationality;
        private DateTime dateofbirth;
        private string cardnumber;
        private bool woman;
        private DateTime expiry;
        private string can;
        private string pesel;
        private string placeofbirth;
        private string familyname;
        private string parents;
        private string authority;
        private DateTime issued;
        private string mrz;
        private BitmapImage photo;
        private readonly Dictionary<char, int> letterValues = new Dictionary<char, int>() {
            { 'A', 10 }, {'B', 11} ,{'C', 12} ,{'D', 13} ,{'E', 14} ,{'F', 15} ,{'G', 16} ,{'H', 17} ,{'I', 18} ,{'J', 19} ,{'K', 20}
            ,{'L', 21} ,{'M', 22} ,{'N', 23} ,{'O', 24} ,{'P', 25} ,{'Q', 26} ,{'R', 27} ,{'S', 28} ,{'T', 29} ,{'U', 30} ,{'V', 31}
            ,{'W', 32} ,{'X', 33} ,{'Y', 34} ,{'Z', 35},{'<', 0},{'0',0},{'1',1},{'2',2},{'3',3},{'4',4},{'5',5},{'6',6},{'7',7},{'8',8},{'9',9}
            };
        Random random = new Random();
        public IdentityCard()
        {
            
        }
        public void generateRandomIdentity()
        {
            setGivenName();
            setSurname();
            setNationality();
            setDateOfBirth(new DateTime(0));
            setIdNumber();
            setDueDate(new DateTime(0));
            setCanNumber();
            setPESEL();
            setBirthPlace();
            setFamilyName();
            setParentsNames();
            setAuthority();
            setIssued(new DateTime(0));
            setPhoto();
            setMRZ();
        }
        public string randomName(char sex = 'a')
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(".names_" + sex + ".txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                if (sex == 'a')
                    return reader.ReadToEnd().Split('\n')[random.Next(0, 625)].TrimEnd('\n', '\r');
                if (sex == 'w')
                    return reader.ReadToEnd().Split('\n')[random.Next(0, 309)].TrimEnd('\n', '\r');
                else
                    return reader.ReadToEnd().Split('\n')[random.Next(0, 315)].TrimEnd('\n', '\r');
            }
        }
        public void setGivenName(char sex = 'a', string newValue=" ")
        {
            if (newValue != " ")
            {
                givenname = newValue; 
                woman = givenname.ToLower().EndsWith("a");
                return;
            }
            givenname = randomName(sex);
            woman = givenname.ToLower().EndsWith("a");
        }
        public string getGivenName()
        {
            return givenname;
        }
        public void setSurname(string newValue=" ")
        {
            if (newValue != " ")
            {
                surname = newValue;
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(woman ? ".woman_surnames.txt" : ".man_surnames.txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                surname = reader.ReadToEnd().Split('\n')[random.Next(0, woman ? 311239 : 298691)];
            }
        }
        public string getSurname()
        {
            return surname;
        }
        public string getSex()
        {
            return woman ? "K" : "M";
        }
        public void setIssued(DateTime newValue)
        {
            if (newValue.Ticks > 0)
            {
                issued = newValue;
                return;
            }
            issued = DateTime.Now.AddDays(-random.Next(1, 1000));
        }
        public DateTime getIssued()
        {
            return issued;
        }
        public void setFamilyName(string newValue = " ")
        {
            if (newValue != " ")
            {
                familyname = newValue;
                return;
            }
            if(woman)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(".woman_surnames.txt"));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    familyname = reader.ReadToEnd().Split('\n')[random.Next(0, 311239)];
                }
            }
            familyname = surname.Split('-')[0];
        }
        public string getFamilyName()
        {
            return familyname;
        }
        public void setParentsNames(string newValue = " ")
        {
            parents = randomName('m') + " " + randomName('w');
        }
        public string getParentsNames()
        {
            return parents;
        }
        public void setNationality(string newValue="POLSKIE")
        {
            if(newValue.Length>=3)
                nationality = newValue;
        }
        public string getNationality()
        {
            return nationality;
        }
        public void setDateOfBirth(DateTime newValue)
        {
            if (newValue.Ticks>0)
            {
                dateofbirth = newValue;
                return;
            }
            dateofbirth = new DateTime(1900, 1, 1).AddDays(random.Next(0, 40000));
        }
        public DateTime getDateOfBirth()
        {
            return dateofbirth;
        }
        public void setDueDate(DateTime newValue)
        {
            if (newValue.Ticks > 0)
            {
                expiry = newValue;
                return;
            }
            expiry = DateTime.Now.AddDays(random.Next(1, 10000));
        }
        public DateTime getDueDate()
        {
            return expiry;
        }
        public void setIdNumber()
        {
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += (char)random.Next(65, 90);
            }
            s += "#";
            s += random.Next(0, 99999).ToString().PadLeft(5, '0');
            int control = 7 * letterValues[s[0]] + 3 * letterValues[s[1]] + 1 * letterValues[s[2]] + 7 * int.Parse(s[4].ToString()) + 3 * int.Parse(s[5].ToString()) + 1 * int.Parse(s[6].ToString()) + 7 * int.Parse(s[7].ToString()) + 3 * int.Parse(s[8].ToString());
            cardnumber = s.Replace('#', (control % 10).ToString()[0]);
        }
        public string getCardNumber()
        {
            return cardnumber;
        }
        public void setCanNumber()
        {
            can = random.Next(0, 999999).ToString().PadLeft(6, '0');
        }
        public string getCanNumber()
        {
            return can;
        }
        public void setPESEL()
        {
            string s = (dateofbirth.Year % 100).ToString().PadLeft(2, '0');
            s += dateofbirth.Year < 2000 ? dateofbirth.Month.ToString().PadLeft(2, '0') : (dateofbirth.Month + 20).ToString();
            s += dateofbirth.Day.ToString().PadLeft(2, '0');
            s += random.Next(0, 999).ToString().PadLeft(3, '0') + (2 * random.Next(0, 4) + (woman ? 0 : 1));
            var weights = new[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var p = int.Parse(s.Substring(i, 1));
                sum += weights[i] * p;
            }
            sum %= 10;
            if (sum > 0)
            {
                sum = 10 - sum;
            }
            s += sum.ToString();
            pesel = s;
        }
        public string getPESEL()
        {
            return pesel;
        }
        public void setBirthPlace(string newValue=" ")
        {
            if (newValue != " ")
            {
                placeofbirth = newValue;
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(".cities.txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                placeofbirth = reader.ReadToEnd().Split('\n')[random.Next(0, 432)];
            }
        }
        public string getBirthPlace()
        {
            return placeofbirth;
        }
        public void setAuthority(string newValue = " ")
        {
            if (newValue != " ")
            {
                authority = newValue;
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(".cities.txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                authority = "PREZYDENT MIASTA " + reader.ReadToEnd().Split('\n')[random.Next(0, 432)];
            }
        }
        public string getAuthority()
        {
            return authority;
        }
        public void setPhoto(BitmapImage newValue=null)
        {
            if (newValue != null)
            {
                photo = newValue;
                return;
            }
            var fullFilePath = @"https://thispersondoesnotexist.com/image";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();
            photo = bitmap;
        }
        public BitmapImage getPhoto()
        {
            return photo;
        }
        private int MRZcheck(string s)
        {
            int sum = 0;
            var weights = new int[3] { 7, 3, 1 };
            for (var i = 0; i < s.Length; i++)
            {
                var p = letterValues[s[i]];
                sum += weights[i % 3] * p;
            }
            return sum % 10;
        }
        public void setMRZ()
        {
            string l1 = "I<POL" + cardnumber + MRZcheck(cardnumber).ToString() + "<<<<<<<<<<<<<<<";
            string dob = (dateofbirth.Year % 100).ToString().PadLeft(2, '0') + dateofbirth.Month.ToString().PadLeft(2, '0') + dateofbirth.Day.ToString().PadLeft(2, '0');
            string exp = (expiry.Year % 100).ToString().PadLeft(2, '0') + expiry.Month.ToString().PadLeft(2, '0') + expiry.Day.ToString().PadLeft(2, '0');
            string l2 = dob + MRZcheck(dob).ToString() + (woman?"K":"M") + exp + MRZcheck(dob).ToString() + nationality.Substring(0,3).ToUpper() +"<<<<<<<<<<<" + MRZcheck(l1.Substring(5) + dob + MRZcheck(dob).ToString() + exp + MRZcheck(dob).ToString()).ToString();
            string l3 = surname.Replace('-', '<').Replace(" ", "<").Replace("\r", "") + "<" + givenname.ToUpper().Replace('-', '<').Replace(" ", "<").Replace("\r", "");
            while (l3.Length < 30)
                l3 += "<";
            mrz = l1 + "\r\n" + l2 + "\r\n" + l3;
        }
        public string getMRZ()
        {
            return mrz;
        }
    }
}
