using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Xml;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp9
{
    class TestProgram
    {
        static Boss[] bosses = new Boss[20];
        static int bosslength { get; set; } = 0;
        static Ishci[] ishciler = new Ishci[20];
        static int ishcilength { get; set; } = 0;

        static int currentIndex { get; set; } = 0;
        static string currentUser { get; set; } = "";

        static void printAllAnnoncements()
        {
            List<Announcement> allAnnouncements = new List<Announcement>();
            for (int i = 0; i < bosslength; i++)
            {
                for (int j = 0; j < bosses[i].annLength; j++)
                {
                    allAnnouncements.Add(bosses[i].announcement[j]);
                }
            }
            int choise = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Is elanin adi : {allAnnouncements[choise].Title}");
            Console.WriteLine($"Sirketin adi : {allAnnouncements[choise].Company}");
            Console.WriteLine($"Kateqoriya : {allAnnouncements[choise].Category}");
            Console.WriteLine($"Is barede melumat : {allAnnouncements[choise].Info}");
            Console.WriteLine($"Seher: {allAnnouncements[choise].City}");
            Console.WriteLine($"Maas: {allAnnouncements[choise].minSalary}");
            Console.WriteLine($"Yas: {allAnnouncements[choise].Age}");
            Console.WriteLine($"Tehsil : {allAnnouncements[choise].Education}");
            Console.WriteLine($"Is tecrubesi : {allAnnouncements[choise].Experience}");
            Console.WriteLine($"Mobil telefon : {allAnnouncements[choise].Phone}");
            Console.WriteLine("Muraciet (y/n)");
            string apply = Console.ReadLine();
            if (apply == "y")
            {
                int hrIndex = 0;
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (allAnnouncements[choise] == bosses[i].announcement[j])
                        {
                            bosses[hrIndex].addApplier(new Appliements(allAnnouncements[choise].Title, ishciler[currentIndex]));
                        }
                    }
                }
            }
            if (apply == "n") { }
        }



        static void SearchByCV()
        {
            for (int i = 0; i < bosslength; i++)
            {
                for (int j = 0; j < bosses[i].annLength; j++)
                {

                    if (bosses[i].announcement[j].minSalary > ishciler[currentIndex].cv.minSalary &&
                        bosses[i].announcement[j].Education == ishciler[currentIndex].cv.Education &&
                        bosses[i].announcement[j].Experience == ishciler[currentIndex].cv.Experience &&
                        bosses[i].announcement[j].Age < ishciler[currentIndex].cv.Age)
                    {
                        Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                        Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                        Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                        Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                        Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                        Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                        Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                        Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                        Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                        Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

                    }
                }
            }
        }

        static bool CheckPass(string pass)
        {
            bool upper = false, digit = false, symbol = false, length = false;
            if (pass.Length < 15)
            {
                length = true;
            }
            for (int i = 0; i < pass.Length; i++)
            {
                if (Char.IsUpper(Convert.ToChar(pass[i])))
                {
                    upper = true;
                }
                if (pass[i] == '-' || pass[i] == '_' || pass[i] == '+' || pass[i] == '/')
                {
                    symbol = true;
                }
            }
            if (Regex.IsMatch(pass, @"\d"))
            {
                digit = true;
            }
            if (upper && digit && symbol && length)
            {
                return true;
            }
            return false;
        }

        static string randomGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Int32.TryParse(Convert.ToString(stringChars[i]), out int a))
                    {

                        var finalString = new String(stringChars);
                        return finalString;
                    }
                    else
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                }
            }

        }

        static string LogIn()
        {
            Console.WriteLine("Username");
            string user = Console.ReadLine();
            Console.WriteLine("Password");
            string pass = Console.ReadLine();
            for (int i = 0; i < bosslength; i++)
            {
                if (bosses[i].Username == user && bosses[i].PassWord == pass)
                {
                    currentIndex = i;
                    return "Boss";
                }
            }
            for (int i = 0; i < ishcilength; i++)
            {
                if (ishciler[i].username == user && ishciler[i].password == pass)
                {
                    currentIndex = i;
                    return "Ishci";
                }
            }
            return "Yanlis Email veya Password";
        }

        static void SearchJob()
        {
            Console.WriteLine("Search by 1.Category \n2.Education\n3.City\n4.Salary\n5.Experience");
            string searchBy = Console.ReadLine();
            if (searchBy == "1")
            {
                Console.WriteLine("Enter Category");
                string category = Console.ReadLine();
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (bosses[i].announcement[j].Category == category)
                        {
                            Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                            Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                            Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                            Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                            Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                            Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                            Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                            Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        }
                    }
                }
            }
            if (searchBy == "2")
            {
                Console.WriteLine("Enter Education");
                string edu = Console.ReadLine();
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (bosses[i].announcement[j].Education == edu)
                        {
                            Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                            Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                            Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                            Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                            Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                            Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                            Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                            Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        }
                    }
                }
            }
            if (searchBy == "3")
            {
                Console.WriteLine("Enter Category");
                string city = Console.ReadLine();
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (bosses[i].announcement[j].City == city)
                        {
                            Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                            Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                            Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                            Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                            Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                            Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                            Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                            Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        }
                    }
                }
            }
            if (searchBy == "4")
            {
                Console.WriteLine("Enter Category");
                string salary = Console.ReadLine();
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (bosses[i].announcement[j].minSalary == Convert.ToInt32(salary))
                        {
                            Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                            Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                            Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                            Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                            Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                            Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                            Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                            Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        }
                    }
                }
            }
            if (searchBy == "5")
            {
                Console.WriteLine("Enter Category");
                string experience = Console.ReadLine();
                for (int i = 0; i < bosslength; i++)
                {
                    for (int j = 0; j < bosses[i].annLength; j++)
                    {
                        if (bosses[i].announcement[j].Experience == experience)
                        {
                            Console.WriteLine($"Is elanin adi : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Sirketin adi : {bosses[i].announcement[j].Company}");
                            Console.WriteLine($"Kateqoriya : {bosses[i].announcement[j].Category}");
                            Console.WriteLine($"Is barede melumat : {bosses[i].announcement[j].Title}");
                            Console.WriteLine($"Seher : {bosses[i].announcement[j].City}");
                            Console.WriteLine($"Maas  : {bosses[i].announcement[j].minSalary}");
                            Console.WriteLine($"Yas : {bosses[i].announcement[j].Age}");
                            Console.WriteLine($"Tehsil  : {bosses[i].announcement[j].Education}");
                            Console.WriteLine($"Is tecrubesi : {bosses[i].announcement[j].Experience}");
                            Console.WriteLine($"Mobil telefon : {bosses[i].announcement[j].Phone}");
                        }
                    }
                }
            }
        }


        static void Register()
        {
            Console.WriteLine("Username");
            string user = Console.ReadLine();
            Console.WriteLine("Email");
            string email = Console.ReadLine();
            Regex mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            while (!mail.IsMatch(email))
            {
                Console.WriteLine("Yanlis Email");
                email = Console.ReadLine();
            }
            Console.WriteLine("Password");
            string pass = Console.ReadLine();
            while (!CheckPass(pass))
            {
                Console.WriteLine("Yanlis Password");

                pass = Console.ReadLine();
            }
            Console.WriteLine("Confirm password");
            string passChecker = Console.ReadLine();
            while (pass != passChecker)
            {
                Console.WriteLine("Yanlis");
                passChecker = Console.ReadLine();
            }
            Console.WriteLine("1.Ishegoturen\n2.Ishci");
            string status = Console.ReadLine();
            switch (status)
            {
                case "1":
                    Boss boss1 = new Boss(user, email, pass);
                    bosses[bosslength++] = boss1;
                    break;
                case "2":
                    Ishci ishci1 = new Ishci(user, email, pass);
                    ishciler[ishcilength++] = ishci1;
                    break;
                default:
                    break;
            }
            string random = randomGenerator();
            Console.WriteLine(random);
            string getRand = Console.ReadLine();
            while (random != getRand)
            {
                Console.WriteLine("Yanlis yeniden...");
                getRand = Console.ReadLine();
            }
        }

        static void writeToFile()
        {
            List<Boss> boss1 = new List<Boss>(bosses);
            List<Ishci> ishci1 = new List<Ishci>(ishciler);
            string Boss = JsonConvert.SerializeObject(boss1);

            string Ishci = JsonConvert.SerializeObject(ishci1);
            using (FileStream str = new FileStream("Ishci.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(str);
                writer.Write(Ishci);
                writer.Close();
            }
            using (FileStream str = new FileStream("Boss.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(str);
                writer.Write(Boss);
                writer.Close();
            }


            Console.WriteLine(Boss);
            Console.WriteLine(Ishci);

        }

        static void readFromFile()
        {
            bosses = new Boss[100];
            ishciler = new Ishci[100];
            ishcilength = 0;
            bosslength = 0;
            List<Ishci> list = new List<Ishci>();
            using (FileStream stream = new FileStream("Employee.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamReader streamReader = new StreamReader(stream);
                string tmp = streamReader.ReadToEnd();
                var a = JsonConvert.DeserializeObject<List<Ishci>>(tmp);
                if (a != null)
                    foreach (var item in a)
                    {
                        list.Add(item as Ishci);
                        ishciler[ishcilength++] = item;
                    }
                streamReader.Close();
            }
            List<Boss> list2 = new List<Boss>();
            using (FileStream stream = new FileStream("HR.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamReader streamReader = new StreamReader(stream);
                string tmp = streamReader.ReadToEnd();
                Console.WriteLine(tmp);
                var a = JsonConvert.DeserializeObject<List<Boss>>(tmp);
                if (a != null)
                    foreach (var item in a)
                    {
                        list2.Add(item as Boss);
                        bosses[bosslength++] = item;
                    }
                streamReader.Close();
            }
        }
        static void BossMenu()
        {
            Console.WriteLine("1.Elan yerlesdir");
            Console.WriteLine("2.Butun muracietleri goster");
            Console.WriteLine("3.Log Out");
            string choise2 = Console.ReadLine();
            if (choise2 == "1")
            {
                bosses[currentIndex].addAnnnouncement();
            }
            if (choise2 == "2")
            {
                bosses[currentIndex].printAllAplliements();
            }
            if (choise2 == "3")
            {
                writeToFile();
                Console.WriteLine("You Logged Out");
                return;
            }

            else
            {
                Console.WriteLine("Wrong Key");
            }
        }

        static void IshciMenu()
        {
            Console.WriteLine("1 - Cv yerlesdir");
            Console.WriteLine("2 - Is axtar (CV melumatlarina gore)");
            Console.WriteLine("3 - Search");
            Console.WriteLine("4 - Melumatlari goster");
            Console.WriteLine("5 - Butun elanlari goster");
            Console.WriteLine("6 - Log out");
            string command3 = Console.ReadLine();
            if (command3 == "1")
            {
                ishciler[currentIndex].addCV();
                IshciMenu();
            }
            if (command3 == "2")
            {
                SearchByCV();
                IshciMenu();
            }
            if (command3 == "3")
            {
                SearchJob();
                IshciMenu();
            }
            if (command3 == "4")
            {
                ishciler[currentIndex].cv.showCv();
                IshciMenu();
            }
            if (command3 == "5")
            {
                printAllAnnoncements();
                IshciMenu();
            }
            if (command3 == "6")
            {
                writeToFile();
                Console.WriteLine("Cixis...");
            }
            else
            {
                Console.WriteLine("Yanlis");
            }

        }

        static void Main()
        {
            readFromFile();

            try
            {
                while (true)
                {
                    Console.WriteLine("1 - Sign up");
                    Console.WriteLine("2 - Sign in");
                    Console.WriteLine("3 - Exit");
                    string command = Console.ReadLine();
                    if (command == "1")
                    {
                        Register();
                    }
                    if (command == "2")
                    {
                        currentUser = LogIn();
                        if (currentUser == "Boss")
                        {
                            BossMenu();
                        }

                        if (currentUser == "Ishci")
                        {
                            IshciMenu();
                        }

                    }
                    if (command == "3")
                    {
                        writeToFile();
                    }
                }
            }
            catch (Exception n)
            {
                Console.WriteLine(n.Message);
            }



        }
    }


    class Ishci
    {
        public string username { get; set; }
        public string email;
        public string password { get; set; }
        public Ishci() { }
        public CV cv = new CV();

        public Ishci(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }

        public void addCV()
        {
            Console.WriteLine("Ad: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Soyad: ");
            string Surname = Console.ReadLine();
            Console.WriteLine("Cins :  (1.Kisi , 2.Qadin)");
            string Gender = Console.ReadLine();
            while (Gender != "1" && Gender != "2")
            {
                Console.WriteLine("Yanlis secim ");
                Gender = Console.ReadLine();
            }
            Console.WriteLine("Yas: ");
            int Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tehsil : (orta , natamam ali, ali)");
            string Education = Console.ReadLine();
            while (Education != "orta" && Education != "natamam ali" && Education != "ali")
            {
                Console.WriteLine("Yanlis secim ");
                Education = Console.ReadLine();
            }
            Console.WriteLine("Is tecrubesi: ");
            string Experience = Console.ReadLine();
            Console.WriteLine("Kateqoriya : ");
            string Category = Console.ReadLine();
            Console.WriteLine("Seher : ");
            string City = Console.ReadLine();
            Console.WriteLine("Minimum emek haqqi: ");
            int minSalary = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Mobil telefon : ");
            string Phone = Console.ReadLine();
            Regex regexphone = new Regex(@"^(055||051||050||070||077)[0-9]{7}$");
            Match match2 = regexphone.Match(Phone);
            if (!match2.Success)
            {
                Console.WriteLine("Yanlis ");
                Phone = Console.ReadLine();
            }
            cv = new CV(Name, Surname, Gender, Age, Education, Experience, Category, City, minSalary, Phone);
        }


    }


    class CV
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public int minSalary { get; set; }
        public string Phone { get; set; }
        public CV() { }
        public CV(string name, string surName, string gender,
            int age, string education, string experience,
            string category, string city, int minSalary, string phone)
        {
            Name = name;
            Surname = surName;
            Gender = gender;
            Age = age;
            Education = education;
            Experience = experience;
            Category = category;
            City = city;
            this.minSalary = minSalary;
            Phone = phone;
        }


        public void showCv()
        {
            Console.WriteLine($"Ad {Name}\nSoyad {Surname}\nCins {Gender}\nYas {Age}\nTehsil {Education}\nTecrube {Experience}\nKateqoriya {Category}\nSeher {City}\nEmek haqqi {minSalary}\nPhone {Phone}");

        }

    }

    class Boss
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public Announcement[] announcement = new Announcement[10];
        public Appliements[] allAppliemets = new Appliements[10];
        public int appliementsLength { get; set; } = 0;
        public int annLength { get; set; } = 0;
        public Boss() { }
        public Boss(string username, string email, string passWord)
        {
            Username = username;
            Email = email;
            PassWord = passWord;
        }
        public void addApplier(Appliements appliements)
        {
            allAppliemets[appliementsLength] = appliements;
            appliementsLength++;
        }

        public void addAnnnouncement()
        {
            Console.WriteLine("Is elanin adi ");
            string Title = Console.ReadLine();
            Console.WriteLine("Sirketin adi ");
            string Company = Console.ReadLine();
            Console.WriteLine("Kateqoriya ");
            string Category = Console.ReadLine();
            Console.WriteLine("Is barede melumat");
            string Info = Console.ReadLine();
            Console.WriteLine("Seher");
            string City = Console.ReadLine();
            Console.WriteLine("Maas ");
            int minSalary = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Yas");
            int Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tehsil (orta, natamam ali, ali)");
            string Education = Console.ReadLine();
            while (Education != "orta" && Education != "natamam ali" && Education != "ali")
            {
                Console.WriteLine("Incorrect Education");
                Education = Console.ReadLine();
            }
            Console.WriteLine("Is tecrubesi ");
            string Experience = Console.ReadLine();
            while (Experience != "1" && Experience != "2" && Experience != "3" && Experience != "4")
            {
                Console.WriteLine("Incorrect Experience");
                Experience = Console.ReadLine();
            }
            Console.WriteLine("Enter Phone");
            string Phone = Console.ReadLine();
            Regex phonecheck = new Regex(@"^(055||051||050||070||077)[0-9]{7}$");
            while (!phonecheck.IsMatch(Phone))
            {
                Console.WriteLine("Wrong Phone");
                Phone = Console.ReadLine();
            }
            Announcement ann = new Announcement(Title, Company, Category, Info, City, minSalary, Age, Education, Experience, Phone);
            announcement[annLength++] = ann;
        }

        public void printAllAplliements()
        {
            for (int i = 0; i < appliementsLength; i++)
            {
                Console.WriteLine($"Is elanin adi : {allAppliemets[i].Title}");
                Console.WriteLine($"Username : {allAppliemets[i].ishciqebulu.username}");
                Console.WriteLine($"Ad : {allAppliemets[i].ishciqebulu.cv.Name}");
                Console.WriteLine($"Soyad : {allAppliemets[i].ishciqebulu.cv.Surname}");
                Console.WriteLine($"Cins : {allAppliemets[i].ishciqebulu.cv.Gender}");
            }
        }
    }


    class Announcement
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public string Info { get; set; }
        public string City { get; set; }
        public int minSalary { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Phone { get; set; }
        public Announcement() { }
        public Announcement(string title, string company,
                            string category, string info,
                            string city, int minSalary, int age,
                            string education, string experience, string phone)
        {
            Title = title;
            Company = company;
            Category = category;
            Info = info;
            City = city;
            this.minSalary = minSalary;
            Age = age;
            Education = education;
            Experience = experience;
            Phone = phone;
        }

    }

    class Appliements
    {
        public string Title { get; set; }
        public Ishci ishciqebulu;
        public Appliements() { }
        public Appliements(string title, Ishci ishciqebulu)
        {
            Title = title;
            this.ishciqebulu = ishciqebulu;
        }
    }
}