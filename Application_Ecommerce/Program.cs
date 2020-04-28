using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application_Ecommerce
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            bool isAuthentifier = false;
            string reponseClient="";
            do
            {
                Console.WriteLine("tapez le nom d'utilisateur");
                string nomUser = Console.ReadLine();
                Task<string> result = InvokeAUthentification(nomUser);
                if(result.Result == "True") { 

                    isAuthentifier = true;
                    Task<string> utilisateur = EnvoieUtilisateur(nomUser);
                }
               
            } while (isAuthentifier == false);

            do
            {
                Console.WriteLine("Veuillez choisir parmis les choix suivants");
                Console.WriteLine("1.Ajouter une quantite dans un stock");
                Console.WriteLine("2.Commander un produit");
                Console.WriteLine("0. pour quitter");
                reponseClient = Console.ReadLine();

                if (reponseClient == "1")
                {
                    Console.WriteLine("Voici les listes des produits que vous pouvez ajouter la quantité:");
                    Console.WriteLine("Tarte" + "/n" + "Millefeuille" + "/n" + "Madeleine");
                    Console.WriteLine("Tapez le nom du produit à ajouter par exemple : Tarte");
                    string produitAjout = Console.ReadLine();
                    Task<string> resultat = AjoutProduit(produitAjout);
                    Console.WriteLine(resultat.Result);
                }
                else if (reponseClient == "2")
                {
                    string message ="";
                    do
                    {
                        Console.WriteLine("Veuillez choisir");
                        Console.WriteLine("Pour commander, veuillez saisir le nom et la quantite du produit par exemple Tarte:1");
                        Console.WriteLine("Pour afficher votre facture, veuillez saisir Facture");
                        
                        message = Console.ReadLine();
                        if (message == "Facture")
                        {
                            Task<string> facture = DemandeFacture(message);
                        }
                        else
                        {
                            Task<string> item = CommandeProduit(message);
                            Task<string> creationFacture = AjouterProduitDansFacture(message);
                            Console.WriteLine(item.Result);
                        }

                    } while (message != "0");
                    
                }
            } while (reponseClient != "0");
            

        }

        private static async Task InvokeAsync(string n)
        {
            var rpcClient = new RpcClient("user_queue");

            Console.WriteLine(" [x] Requesting fib({0})", n);
            var response = await rpcClient.CallAsync(n.ToString());
            Console.WriteLine(" [.] Got '{0}'", response);

            rpcClient.Close();
        }

        //Envoie du nom de l'utilisateur vers le service UserManager
        public static async Task<string> InvokeAUthentification(string nom)
        {
            var rpcClient = new RpcClient("user_queue");
            string response = await rpcClient.CallAsync(nom);
            return response;
        }

        public static async Task<string> CommandeProduit(string message)
        {
            
            var rpcClient = new RpcClient("stock_queue");
            string response = await rpcClient.CallAsync("2:"+ message);
            return response;
        }

        public static async Task<string> AjoutProduit(string message)
        {
            var rpcClient = new RpcClient("stock_queue");
            string response = await rpcClient.CallAsync("1:"+message);
            return response;

        }
        public static async Task<string>AjouterProduitDansFacture(string message)
        {
            var rpcClient = new RpcClient("bill_queue");
            string response = await rpcClient.CallAsync(message);
            return response;
        }

        public static async Task<string> DemandeFacture(string message)
        {
            var rpcClient = new RpcClient("bill_queue");
            string response = await rpcClient.CallAsync(message);
            return response;
        }

        public static async Task<string> EnvoieUtilisateur(string message)
        {
            var rpcClient = new RpcClient("bill_queue");
            string response = await rpcClient.CallAsync("User:"+ message);
            return response;
        }


    }
}

