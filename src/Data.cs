
using System.Text.Json;

namespace ITPHG.Models;
{
    // Einfaches JSON-Repository
    public class Data
    {
    private const string FileName = "stammbaum_db.json";
    private List<User> _users = new List<User>();
    private List<Person> _persons = new List<Person>();
        
        
    public List<User> Users
    {
        get
        {
            return _users;
        }
        set
        {
            _users = value;
        }
    }
    
    public List<Person> Persons
    {
        get
        {
            return _persons;
        }
        set
        {
            _persons = value;
        }
    }


    public Data()
    {
        Load();
    }

        public void Load()
        {
            if (!File.Exists(FileName))
            {
                Save();
                return;
            }

            try
            {
                var json = File.ReadAllText(FileName);
                var doc = JsonSerializer.Deserialize<StorageDto>(json);
                if (doc != null)
                {
                    Users = doc.Users ?? new List<User>();
                    Persons = doc.Persons ?? new List<Person>();
                }
            }
            catch
            {
                // bei Fehler: leere DB
                Users = new List<User>();
                Persons = new List<Person>();
            }
        }

        public void Save()
        {
            var dto = new StorageDto { Users = Users, Persons = Persons };
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FileName, JsonSerializer.Serialize(dto, options));
        }

        private class StorageDto
        {
            public List<User>? Users { get; set; }
            public List<Person>? Persons { get; set; }
        }
    }
}
