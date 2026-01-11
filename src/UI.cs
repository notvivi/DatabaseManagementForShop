using DbProjekt.DAO;
using DbProjekt.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DbProjekt.src
{
    /// <summary>
    /// Class for whole ui 
    /// </summary>
    public class UI
    {
        CustomerDAO customers = new CustomerDAO();
        CommissionDAO commissions = new CommissionDAO();
        RaceDAO races = new RaceDAO();
        ListDAO listArtif = new ListDAO();
        ArtifactDAO artifacts = new ArtifactDAO();
    
        /// <summary>
        /// Constructor
        /// </summary>
        public UI() 
        {
        }

        /// <summary>
        /// Method for importing data from file
        /// </summary>
        public void Import()
        {
            Console.WriteLine("Do you want to import data?");
            Console.WriteLine("y -> wants to import data."); 
            Console.WriteLine("anything else -> doesnt want to import data.");


            string choice = Console.ReadLine();

            if (choice?.Trim().ToLower() == "y")
            {
                string artifactFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "artifacts.csv");
                string raceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "races.csv");

                if (!File.Exists(artifactFile) || !File.Exists(raceFile))
                {
                    Console.WriteLine("CSV files not found.");
                    Console.WriteLine("Make sure artifacts.csv and races.csv are next to the exe file.");
                } else
                {
                    Importer im = new Importer();
                    im.ImportCsv(artifactFile, raceFile);
                }

            }
            Start();
        }

        /// <summary>
        /// Method that prints the header of whole Ui
        /// </summary>
        public void Start()
    {
            bool shopping = true;

            while(shopping == true)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("<WELCOME TO MY ARTIFACT SHOP>");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Offers and price list");
                Console.WriteLine("-----------------------------");
                ArtifactList();
                Console.WriteLine("-----------------------------");

                Console.WriteLine("What do you want to do?");
                Console.WriteLine("0.Close program");
                Console.WriteLine("1.Create new order");
                Console.WriteLine("2.Cancel your order");
                Console.WriteLine("3.Edit your order");
                Console.WriteLine("4.Create new customer");
                Console.WriteLine("5.Show statistics");
                string choice = Console.ReadLine();
                Choice(choice);

                if (choice == "0")
                {
                    shopping = false;
                }
            }
       
        }
        /// <summary>
        /// Method that switches through different states and methods
        /// </summary>
        /// <param name="choice"></param>
        public void Choice(string choice)
        {
          
            switch (choice)
            {
                case "0":
                    Console.WriteLine("Program ended");
                    break;
                case "1":
                    Console.WriteLine("Creating order");
                    CreateOrder();
                    break;
                case "2":
                    Console.WriteLine("Canceling order");
                    CancelOrder();
                    break;
                case "3":
                    Console.WriteLine("Editing order");
                    EditOrder();
                    break;  
                case "4":
                    Console.WriteLine("Creating customer.");
                    CreateCustomer();
                    break;
                case "5":
                    ShowStats();
                    break;
                default:
                    return;   
            }
        }
        /// <summary>
        /// Method for creating order
        /// </summary>
        public void CreateOrder()
        {

            if (listArtif.GetAll().Count() == 0)
            {
                Console.WriteLine("No artifacts in list were loaded.");
                return;
            }


            Console.WriteLine("-----------------------------");
            Console.WriteLine("Write number of the product you want.");

            int artifact_id;
            bool exists = false;

            do {
                artifact_id = getNumber();
                exists = listArtif.GetAll().Any(i => i.ID == artifact_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);

            if (customers.GetAll().Count() == 0)
            {
                Console.WriteLine("No customers have been created yet.");
                return;
            }
            else
            {
                Console.WriteLine("Choose which customer you are.");

                foreach (Customer c in customers.GetAll())
                {
                    Console.WriteLine(c);
                }
            }

            int customer_id;

            do
            {
                customer_id = getNumber();
                exists = customers.GetAll().Any(i => i.ID == customer_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);

            new CommissionService().CreateCommission(artifact_id, customer_id);
        }

        /// <summary>
        /// Method for canceling order
        /// </summary>
        public void CancelOrder()
        {

            Console.WriteLine("-----------------------------");
            Console.WriteLine("Write number of the order you want to cancel.");

            if (commissions.GetAll().Count() == 0)
            {
                Console.WriteLine("No orders have been created yet.");
                return;
            }
            else
            {
                foreach (Commission c in commissions.GetAll())
                {

                    Console.WriteLine(c);
                }
            }

            int order_id;
            bool exists;

            do
            {
                order_id = getNumber();
                exists = commissions.GetAll().Any(i => i.ID == order_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);

            commissions.Cancel(commissions.GetByID(order_id));

        }
        /// <summary>
        /// Method for editing order
        /// </summary>
        public void EditOrder()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Write number of the order you want to edit.");

            if (commissions.GetAll().Count() == 0)
            {
                Console.WriteLine("No orders have been created yet.");
                return;
            }
            else
            {
                foreach (Commission c in commissions.GetAll())
                {

                    Console.WriteLine(c);
                }
            }

            int order_id;
            bool exists;    

            do
            {
                order_id = getNumber();
                exists = commissions.GetAll().Any(i => i.ID == order_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);


            Commission commission = commissions.GetByID(order_id);
            if (commission == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.WriteLine("Write number of new artifact that you want.");


            int list_id;

            do
            {
                list_id = getNumber();
                exists = listArtif.GetAll().Any(i => i.ID == list_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);


            Commission update = new Commission(commission.ID,list_id,commission.Customer_id, DateTime.Now);

            commissions.Save(update);

        }
        /// <summary>
        /// Method for creating customer
        /// </summary>
        public void CreateCustomer()
        {
            Console.WriteLine("Choose your nickname:");
            
            string nickname;
            bool exists;

            do
            {
                nickname = Console.ReadLine()?.Trim();
                exists = customers.GetAll().Any(i => i.Nickname == nickname);

                if (string.IsNullOrEmpty(nickname))
                {
                    Console.WriteLine("Nickname cannot be empty.");
                    Console.WriteLine("Try again.");

                }
                else if (nickname.Length > 20)
                {
                    Console.WriteLine("Nickname is longer then 20.");
                    Console.WriteLine("Try again.");
                }
                else if (exists)
                {
                    Console.WriteLine("Nickname has been already taken.");
                    Console.WriteLine("Choose another one.");
                }

            } while (exists || string.IsNullOrEmpty(nickname) || nickname.Length > 20);


            Console.WriteLine("Write a number of race you are");

            if(races.GetAll().Count() == 0) 
            {
                Console.WriteLine("No races have been created yet.");
                return;
            }
            else
            {
                foreach (Race race in races.GetAll())
                {
                    Console.WriteLine(race);
                }

            }

            int race_id;
           
            do
            {
                race_id = getNumber();
                exists = races.GetAll().Any(i => i.ID == race_id);

                if (exists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Id wasnt found");
                }

            } while (!exists);

            try
            {
                Customer c1 = new Customer(nickname, race_id);
                customers.Save(c1);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                try
                {
                    DatabaseSingleton.CloseConnection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        /// <summary>
        /// Method for showing statistics for an user
        /// </summary>
        public void ShowStats()
        {
            var allCustomers = customers.GetAll().ToList();

            if (allCustomers.Count == 0)
            {
                Console.WriteLine("No customers have been created yet.");
                return;
            }

            Console.WriteLine("Choose which customer you are:");
            foreach (var c in allCustomers)
            {
                Console.WriteLine(c);
            }

            int customerId;
            bool exists;
            do
            {
                customerId = getNumber();
                exists = allCustomers.Any(i => i.ID == customerId);
                if (!exists)
                    Console.WriteLine("Id wasn't found. Try again.");
            } while (!exists);

            try
            {
                StatsDAO statsDAO = new StatsDAO();
                Stats stats = statsDAO.GetStats(customerId);

                Console.WriteLine("----- STATISTIKY -----");
                Console.WriteLine(stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nepodařilo se načíst statistiky pro tohoto zákazníka: " + ex.Message);
            }
        }

        /// <summary>
        /// Method for not getting null input 
        /// Source: My friend helped me with this one
        /// </summary> 
        /// <returns>number that is not null</returns>
        public int getNumber() 
        {
            bool exit = false;
            int num = 0;

            while (!exit)
            {
                Console.WriteLine("Write number.");
                string input = Console.ReadLine();
                exit = int.TryParse(input, out num);
               
            }

            return num;
        }
        /// <summary>
        /// Method for displaying artifacts on console
        /// </summary>
        public void ArtifactList()
        {
            if(listArtif.GetAll().Count() == 0)
            {
                Console.WriteLine("Entry list is empty or inserting to list went wrong.");
            }

            foreach (List l in listArtif.GetAll())
            {
                Console.WriteLine(l);
            }
        }
    }
}
