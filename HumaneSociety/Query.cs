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
        public delegate void EmployeeVoidFunciton(Employee employee);

        internal static Room GetRoom(int animalID)
        {
            var room = db.Rooms.Where(r => r.AnimalId == animalID).Single();
            return room;
        }
        internal static List<Adoption> GetPendingAdoptions()
        {
            var adoption = db.Adoptions.Where(a => a.ApprovalStatus == "pending");
            return adoption.ToList();
        }
        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }
        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits(Dictionary<int, string> searchParameters)
        {
            var animals = from data in db.Animals select data;
            if (searchParameters.ContainsKey(1))
            {
                animals = (from animal in animals where animal.Species.Name == searchParameters[1] select animal);
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
        internal static IEnumerable<AnimalShot> GetShots(Animal animal)
        {
            var shots = db.AnimalShots.Where(a => a.AnimalId == a.AnimalId);
            return shots;
        }
        internal static void UpdateShot(string shot, Animal animal)
        {
            AnimalShot currentShot = new AnimalShot
            {
                Animal = animal,
                AnimalId = animal.AnimalId,
                DateReceived = DateTime.Now
            };
            currentShot.Shot = new Shot
            {
                ShotId = db.Shots.Where(s => s.Name == shot).Select(s => s.ShotId).Single()
            };
            db.AnimalShots.InsertOnSubmit(currentShot);
            db.SubmitChanges();
        }
        internal static void GiveShots(Animal animal, List<AnimalShot> shotsNotUpToDate)
        {
            var shots = db.Shots;
            DateTime dateTime = DateTime.Now;
            if(shotsNotUpToDate.Count < 1)
            {
                var shotsList = shots.ToList();
                for(int i = 0; i < 5; i++)
                {
                    AnimalShot animalShot = new AnimalShot();
                    animalShot.ShotId = shotsList[i].ShotId;
                    animalShot.AnimalId = animal.AnimalId;
                    animalShot.DateReceived = dateTime.Date;
                    db.AnimalShots.InsertOnSubmit(animalShot);
                }
            }
        }
        internal static void CreateNewSpecies(string speciesName)
        {
            Species newSpecies = new Species();
            {
                newSpecies.Name = speciesName;
            }
            db.Species.InsertOnSubmit(newSpecies);
            db.SubmitChanges();
        }
        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            var updateAnimal = db.Animals.Single(a => a.AnimalId == animal.AnimalId);
            if (updates.ContainsKey(1))
            {
                var availableSpecies = db.Species.Single(s => updates[1].ToLower() == s.Name.ToLower());
                if(availableSpecies == null)
                {
                    CreateNewSpecies(updates[1].ToLower());
                    EnterUpdate(animal, updates);
                    return;
                }
                else
                {
                    updateAnimal.SpeciesId = availableSpecies.SpeciesId;
                }
            }
            if (updates.ContainsKey(2))
            {
                updateAnimal.Name = updates[2];
            }
            if (updates.ContainsKey(3))
            {
                updateAnimal.Age = int.Parse(updates[3]);
            }
            if (updates.ContainsKey(4))
            {
                updateAnimal.Demeanor = updates[4];
            }
            if (updates.ContainsKey(5))
            {
                if(updates[5].ToLower() == "true")
                {
                    updateAnimal.KidFriendly = true;
                }
                else if(updates[5].ToLower() == "false")
                {
                    updateAnimal.KidFriendly = false;
                }
            }
            if (updates.ContainsKey(6))
            {
                if(updates[6].ToLower() == "true")
                {
                    updateAnimal.PetFriendly = true;
                }
                else if(updates[6].ToLower() == "false")
                {
                    updateAnimal.PetFriendly = false;
                }
            }
            if (updates.ContainsKey(7))
            {
                updateAnimal.Weight = int.Parse(updates[7]);
            }
            if (updates.ContainsKey(8))
            {
                UpdateRoom(updateAnimal, updates[8]);
            }
            db.SubmitChanges();
            return;
        }
        internal static void UpdateRoom(Animal updateAnimal, string inputNumber)
        {
            int roomNumber = int.Parse(inputNumber);
            Room updateRoom = db.Rooms.Single(r => r.AnimalId == updateAnimal.AnimalId);
            if(updateRoom != null)
            {
                updateRoom.RoomNumber = roomNumber;
                db.SubmitChanges();
            } 
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
            string speciesName = UserInterface.GetStringData("species", "Animal's");
            if(!IsSpeciesInTable(speciesName))
            {
                CreateNewSpecies(speciesName);
            }
            var currentSpecies = db.Species.Distinct().Single(s => s.Name.ToLower() == speciesName.ToLower());
            return currentSpecies;
            
        }
        private static bool IsSpeciesInTable(string compareStrings)
        {
            if(db.Species.Where(s => s.Name.ToLower() == compareStrings) !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static DietPlan GetDietPlan()
        {
            DietPlan dietPlan = new DietPlan();
            dietPlan.DietPlanId = UserInterface.GetIntegerData();
            dietPlan.Name = UserInterface.GetStringData("species", "the animal's");
            dietPlan.FoodType = UserInterface.GetStringData("food type", "the animal's");
            dietPlan.FoodAmountInCups = UserInterface.GetIntegerData();
            return dietPlan;
        }
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Employee EmployeeLogin(string userName, string password)
        {
            var employee = db.Employees.Single(e => userName == e.UserName && password == e.Password);
            return employee;
        }
        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee newEmployee = new Employee();
            {
                newEmployee.Email = email;
                newEmployee.EmployeeNumber = employeeNumber;                
            };
            db.Employees.InsertOnSubmit(newEmployee);
            db.SubmitChanges();
            return newEmployee;  
        }
        internal static void AddUsernameAndPassword(Employee employee)
        {
            var foundEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).First();
            foundEmployee.UserName = foundEmployee.UserName;
            db.SubmitChanges();
            db.Employees.Where(e => e.Password == employee.Password);
            foundEmployee.Password = employee.Password;
            db.SubmitChanges();
        }
        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            var exists = db.Employees.Where(s => s.UserName == userName).SingleOrDefault();
            if (exists != default(Employee))
            {
                return true;
            }
            else
                return false;
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
        internal static void RunEmployeeQueries(Employee employee, string input)
        {
            EmployeeVoidFunciton doTheQueryStuff;
            switch (input)
            {
                case "create":
                    doTheQueryStuff = AddEmployee;
                    break;
                case "read":
                    doTheQueryStuff = ReadEmployee;
                    break;
                case "update":
                    doTheQueryStuff = UpdateEmployee;
                    break;
                case "delete":
                    doTheQueryStuff = DeleteEmployee;
                    break;
                default:
                    throw new ApplicationException("An unexpected error occured. Please contact support.");
            }
            doTheQueryStuff(employee);
        }
        internal static void AddEmployee(Employee employee)
        {
            db.Employees.InsertOnSubmit(employee);
            db.SubmitChanges();
        }
        internal static void ReadEmployee(Employee employee)
        {
            var employeeToRead = db.Employees.SingleOrDefault(Employee => employee.EmployeeId == Employee.EmployeeId);

            Console.WriteLine("Firstname: " + employeeToRead.FirstName);
            Console.WriteLine("Lastname: " + employeeToRead.LastName);
            Console.WriteLine("Username: " + employeeToRead.UserName);
            Console.WriteLine("Password: " + employeeToRead.Password);
            Console.WriteLine("Email: " + employeeToRead.Email);
        }
        internal static void UpdateEmployee(Employee employee)
        {
            var currentEmployee = db.Employees.Distinct().Single(e => e.EmployeeId == e.EmployeeId);
            currentEmployee.FirstName = employee.FirstName;
            currentEmployee.LastName = employee.LastName;
            currentEmployee.Email = employee.Email;
            currentEmployee.UserName = employee.UserName;
            currentEmployee.Password = employee.Password;
            currentEmployee.EmployeeNumber = employee.EmployeeNumber;
            db.SubmitChanges();
        }
        internal static void DeleteEmployee(Employee employee)
        {
            var employeeToDelete = db.Employees.SingleOrDefault(user => user.EmployeeId == employee.EmployeeId);
            if (employeeToDelete != null)
            {
                db.Employees.DeleteOnSubmit(employeeToDelete);
                db.SubmitChanges();
            }
        }
        internal static List<Adoption> GetUserAdoptionStatus(Client client)
        {
            var adoptions = db.Adoptions.Where(c => c.ClientId == client.ClientId);
            return adoptions.ToList();
        }
        internal static IQueryable<Client> RetrieveClients()
        {
            var currentClients = db.Clients.Select(Client => Client);
            return currentClients;
        }
        internal static Animal GetAnimalByID(int iD)
        {
            var desiredAnimal = db.Animals.Single(a => a.AnimalId == iD);
            return desiredAnimal;
        }
        internal static void Adopt(Animal animal, Client client)
        {
            var selectedAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();
            selectedAnimal.AdoptionStatus = "Pending";
            Adoption adoption = new Adoption()
            {
                ClientId = client.ClientId,
                AnimalId = animal.AnimalId,
                ApprovalStatus = "pending",
                AdoptionFee = 75,
                PaymentCollected = true
            };
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }
        internal static void UpdateAdoptionStatus(bool adopted, Adoption adoption)
        {
            var isAdopted = db.Adoptions.Where(a => a.AdoptionId == adoption.AdoptionId).Single();
            if (adopted)
            {
                isAdopted.ApprovalStatus = "adopted";
            }
            else
            {
                isAdopted.ApprovalStatus = "not adopted";
            }
            db.SubmitChanges();
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
