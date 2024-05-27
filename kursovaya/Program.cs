using System;
using System.Collections.Generic;
using System.IO;

public class Contact
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}

public class Phonebook
{
    private List<Contact> contacts;
    private string filePath;

    public Phonebook(string filePath)
    {
        this.filePath = filePath;
        contacts = LoadContacts();
    }

    private List<Contact> LoadContacts()
    {
        List<Contact> loadedContacts = new List<Contact>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    Contact contact = new Contact
                    {
                        Name = parts[0],
                        PhoneNumber = parts[1]
                    };

                    loadedContacts.Add(contact);
                }
            }
        }

        return loadedContacts;
    }

    private void SaveContacts()
    {
        List<string> lines = new List<string>();

        foreach (Contact contact in contacts)
        {
            string line = $"{contact.Name},{contact.PhoneNumber}";
            lines.Add(line);
        }

        File.WriteAllLines(filePath, lines);
    }



    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
        SaveContacts();
    }



    public bool RemoveContact(Contact contact)
    {
        Contact contactToRemove = contacts.Find(c => c.Name == contact.Name && c.PhoneNumber == contact.PhoneNumber);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            SaveContacts();
            return true;
        }

        return false;
    }



    public List<Contact> GetAllContacts()
    {
        return contacts;
    }
}



public class Program
{
    public static void Main()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Вас вiтає Телефонна книга");

        Phonebook phonebook = new Phonebook("phonebook.txt");

        while (true)
        {
            Console.WriteLine("\nВиберiть функцiю:");
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║           Меню вибору           ║");
            Console.WriteLine("╠═════════════════════════════════╣");
            Console.WriteLine("║ 1. Додати контакт               ║");
            Console.WriteLine("║ 2. Показати всi контакти списку ║");
            Console.WriteLine("║ 3. Видалити контакт             ║");
            Console.WriteLine("║ 4. Вийти                        ║");
            Console.WriteLine("╚═════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Введiть номер обраної дiї: ");
            Console.ForegroundColor = ConsoleColor.White;
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddContact(phonebook);
                        break;
                    case 2:
                        ShowAllContacts(phonebook);
                        break;
                    case 3:
                        RemoveContact(phonebook);
                        break;
                    case 4:
                        Console.WriteLine("\nДякуємо за користання телефонним довiдником. Гарного дня!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nНеправильний вибiр. Оберіть дiю зi списку.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }


            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНеправильний вибiр. Оберіть дiю зi списку.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }




    private static void AddContact(Phonebook phonebook)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nДодання нового контакту:");
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("Введiть iмя: ");
        string name = Console.ReadLine();



        Console.Write("Введiть номер телефону: ");
        string phoneNumber = Console.ReadLine();

        Contact contact = new Contact
        {
            Name = name,
            PhoneNumber = phoneNumber
        };



        phonebook.AddContact(contact);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nКонтакт успiшно додано в телефонний довiдник!");
        Console.ForegroundColor = ConsoleColor.White;


    }

    private static void ShowAllContacts(Phonebook phonebook)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nВсi контакти якi заповненi в телефонному довiднику:");
        Console.ForegroundColor = ConsoleColor.White;

        List<Contact> contacts = phonebook.GetAllContacts();

        if (contacts.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("В телефонному довiднику поки що немає жодного контакта.");
            Console.ForegroundColor = ConsoleColor.White;
        }


        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    Список контактiв                   ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
            Console.WriteLine("║           Iмя           ║       Номер телефону        ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"║ {contact.Name,-23} ║ {contact.PhoneNumber,-28} ║");
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }



    private static void RemoveContact(Phonebook phonebook)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nВидалення контакту:");
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("Введiть iмя контакту, котрого хочете видалити: ");
        string name = Console.ReadLine();

        Console.Write("Введiть номер контакту, котрого хочете видалити: ");
        string phoneNumber = Console.ReadLine();



        Contact contact = new Contact
        {
            Name = name,
            PhoneNumber = phoneNumber
        };

        bool removed = phonebook.RemoveContact(contact);

        if (removed)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nКонтакт видалено успiшно!");
        }

        else

        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nКонтакт не знайдено. Видалення не виповнено.");
        }



        Console.ForegroundColor = ConsoleColor.White;


    }




}
