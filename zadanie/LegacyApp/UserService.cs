using System;

namespace LegacyApp
{
    public class UserService
    {

        private ClientRepository clientRepository;
        private UserCreditService userCreditService;

        public UserService()
        {
            this.userCreditService = new UserCreditService();
            this.clientRepository = new ClientRepository();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            
            // Check if all fields are filled correctly and age is above 21
            if (!checkNames(firstName, lastName) || !checkEmail(email) || !checkAge(dateOfBirth))
            {
                return false;
            }
            
            // Get client by id from repository
            var client = clientRepository.GetById(clientId);
            if (client == null)
            {
                // throw new ArgumentException("Client does not exist");
                return false;
            }

            var user = CreateUser(firstName, lastName, email, client, dateOfBirth);
            
            // set credit limit based on client type
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    user.HasCreditLimit = true;
                    user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
                    break;
                case "NormalClient":
                    user.HasCreditLimit = true;
                    user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    break;
                default:
                    throw new ArgumentException("Client type not handled");
                    break;
            }

            // check if credit limit is above 500
            if (!checkCreditLimit(user)) return false;

            // add user to database
            UserDataAccess.AddUser(user);
            return true;
            
        }
        
        User CreateUser(string firstName, string lastName, string email, Client client, DateTime dateOfBirth)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }
        
        bool checkNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }
        
        bool checkEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }
        
        int CalculateAge(DateTime dateTime)
        {
            var now = DateTime.Now;
            int age = now.Year - dateTime.Year;
            if (now.Month < dateTime.Month || (now.Month == dateTime.Month && now.Day < dateTime.Day)) age--;
            return age;
        }

        bool checkAge(DateTime dateOfBirth)
        {
            var age = CalculateAge(dateOfBirth);
            if (age < 21)
            {
                return false;
            }
            return true;
        }
        
        bool checkCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            return true;
        }
        
        
        
        
        
        
    }
    
}
