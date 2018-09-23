using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        private static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        internal static Room GetRoom(int animalId)
        {
            var room = db.Rooms.Where(a => a.AnimalId == animalId).Single();
            return room;
        }
        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            IQueryable<Adoption> adoptions = null;
            return adoptions;

        }
        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }
        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits(Dictionary<int, string> searchParameters)
        {
            var animals = from data in db.Animals select data;

            searchParameters = UserInterface.GetAnimalCriteria();
            if (searchParameters.ContainsKey(1))
            {
                animals = (from animal in animals where animal.species.Name == searchParameters[1] select animal);
            }
            if (searchParameters.ContainsKey(2))
            {
                animals = (from animal in animals where animal.Name == searchParameters[2] select animal);
            }
            if (searchParameters.ContainsKey(3))
            {
                animals = (from animal in animals where animal.Age == int.Parse(searchParameters[3]) select animal);
            }
            if (searchParameters.ContainsKey(4))
            {
                animals = (from animal in animals where animal.Demeanor == searchParameters[4] select animal);
            }
            if (searchParameters.ContainsKey(5))
            {
                bool parameter = GetBoolParamater(searchParameters[5]);
                animals = (from animal in animals where animal.KidFriendly == parameter select animal);
            }
            if (searchParameters.ContainsKey(6))
            {
                bool parameter = GetBoolParamater(searchParameters[6]);
                animals = (from animal in animals where animal.PetFriendly == parameter select animal);
            }
            if (searchParameters.ContainsKey(7))
            {
                animals = (from animal in animals where animal.Weight == int.Parse(searchParameters[7]) select animal);
            }
            if (searchParameters.ContainsKey(8))
            {
                animals = (from animal in animals where animal.AnimalId == int.Parse(searchParameters[8]) select animal);
            }
            return animals;
        }

        private static bool GetBoolParamater(string input)
        {
            if (input.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            IQueryable<AnimalShot> shots = null;
            return shots;

        }
        internal static void UpdateShot(string v, Animal animal)
        {
            throw new NotImplementedException();
        }
        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            throw new NotImplementedException();
        }
        internal static Client GetClient(string userName, string password)
        {
            var currentClient = db.Clients.Distinct().Single(c => c.UserName == userName && c.Password == password);
            return currentClient;

        }
        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Species GetSpecies()
        {
            throw new NotImplementedException();
        }
        internal static DietPlan GetDietPlan()
        {
            throw new NotImplementedException();
        }
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Employee EmployeeLogin(string userName, string password)
        {
            throw new NotImplementedException();
        }
        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            throw new NotImplementedException();
        }
        internal static void AddUsernameAndPassword(Employee employee)
        {
            throw new NotImplementedException();
        }
        internal static bool CheckEmployeeUserNameExist(string username)
        {
            throw new NotImplementedException();
        }
        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Client newClient = new Client();
            {
                newClient.FirstName = firstName;
                newClient.LastName = lastName;
                newClient.UserName = username;
                newClient.Password = password;
                newClient.Email = email;
            };
            newClient.Address = new Address();
            {
                newClient.Address.AddressLine1 = streetAddress;
                newClient.Address.Zipcode = zipCode;
                newClient.Address.USStateId = state;
            };
            db.Clients.InsertOnSubmit(newClient);
            db.Addresses.InsertOnSubmit(newClient.Address);
            db.SubmitChanges();
        }
        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }
        internal static IQueryable<Adoption> GetUserAdoptionStatus(Client client)
        {
            IQueryable<Adoption> clients = null;
            return clients;
        }
        internal static IQueryable<Client> RetrieveClients()
        {
            var currentClients = db.Clients.Select(Client => Client);
            return currentClients;
        }
        internal static Animal GetAnimalByID(int iD)
        {
            Animal animal = null;
            return animal;
        }
        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }
        internal static IQueryable<USState> GetStates()
        {
            var currentStates = db.USStates.Select(States => States);
            return currentStates;
        }
        internal static void UpdateClient(Client client)
        {
            var foundClient = db.Clients.Where(c => c.ClientId == client.ClientId).FirstOrDefault();
            foundClient = client;
            db.SubmitChanges();
        }
        internal static void UpdateUsername(Client client)
        {
            var foundClient = db.Clients.Where(u => u.ClientId == client.ClientId).FirstOrDefault();
            foundClient.UserName = client.UserName;
            db.SubmitChanges();
        }
        internal static void UpdateEmail(Client client)
        {
            var foundClient = db.Clients.Where(e => e.ClientId == client.ClientId).FirstOrDefault();
            foundClient.Email = client.Email;
            db.SubmitChanges();
        }
        internal static void UpdateAddress(Client client)
        {
            var foundClient = db.Clients.Where(a => a.ClientId == client.ClientId).FirstOrDefault();
            foundClient.Address = client.Address;
            db.SubmitChanges();
        }
        internal static void UpdateFirstName(Client client)
        {
            var foundClient = db.Clients.Where(f => f.ClientId == client.ClientId).FirstOrDefault();
            foundClient.FirstName = client.FirstName;
            db.SubmitChanges();
        }
        internal static void UpdateLastName(Client client)
        {
            var foundClient = db.Clients.Where(l => l.ClientId == client.ClientId).FirstOrDefault();
            foundClient.LastName = client.LastName;
            db.SubmitChanges();
        }
    }
}
