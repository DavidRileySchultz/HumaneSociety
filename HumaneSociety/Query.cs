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
        internal static object GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }
        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }
        internal static object SearchForAnimalByMultipleTraits()
        {
            throw new NotImplementedException();
        }
        internal static object GetShots(Animal animal)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Species GetSpecies()
        {
            db.Species.
        }
        internal static DietPlan GetDietPlan()
        {
            throw new NotImplementedException();
        }
        internal static void AddAnimal(Animal animal)
        {
            throw new NotImplementedException();
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
        internal static object RetrAddNewClientieveClients()
        {
            throw new NotImplementedException();
        }
        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            Address address = new Address();
            address.AddressLine1 = streetAddress;
            address.Zipcode = zipCode;
            address.USState = state;
            db.SubmitChanges();

        }
        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }
        internal static object GetUserAdoptionStatus(Client client)
        {
            throw new NotImplementedException();
        }
        internal static object RetrieveClients()
        {
            throw new NotImplementedException();
        }
        internal static object GetAnimalByID(int iD)
        {
            throw new NotImplementedException();
        }
        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }
        internal static object GetStates()
        {
            throw new NotImplementedException();
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
