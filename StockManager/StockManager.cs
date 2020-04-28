using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StockManager
{
    public class StockManager
    {

        public static List<ItemLine> stock = new List<ItemLine>();
        //private const string ItemsFolder = @"C:\Users\tsile\Documents\A5\PATRON\TP5\Application_Ecommerce\Commun\ItemLineFile.json";
        public static  string path = System.IO.Directory.GetCurrentDirectory();
        public static string ItemsFolder = path + @"\Commun\ItemLineFile.json";






        public static List<ItemLine> getStock()
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(ItemsFolder))
            {
                

                string json = r.ReadToEnd();
                stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemLine>>(json);
            }
            return stock;
        }
        public StockManager()
        {
            
        }

        public string ReserveItem(int quantity, string nomProduit)
        {

            using (System.IO.StreamReader r = new System.IO.StreamReader(ItemsFolder))
            {

                string json = r.ReadToEnd();
                stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemLine>>(json);
            }
            ItemLine selectionne = null;

            foreach (ItemLine item in stock)
            {
              
                if (item.item.nom ==nomProduit && (item.quantity - quantity) >= 0) {

                    selectionne = item;
                   
                }
            }
            if (selectionne != null)
            {
                selectionne.quantity -= quantity;
             
                

                string json = JsonConvert.SerializeObject(stock.ToArray());

                //write string to file
                System.IO.File.WriteAllText(ItemsFolder, json);
                Console.WriteLine("Ecriture fichier réussi");
                return selectionne.item.nom + " " + selectionne.quantity;
                

            }
            else return "aucun objet";

            
           
        }


        public string ReleaseItem(string nomProduit)
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(ItemsFolder))
            {

                string json = r.ReadToEnd();
                stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemLine>>(json);
            }
            ItemLine selectionne = null;

            foreach (ItemLine item in stock)
            {

                if (item.item.nom == nomProduit)
                {

                    selectionne = item;

                }
            }
            if (selectionne != null)
            {
                selectionne.quantity = 20;



                string json = JsonConvert.SerializeObject(stock.ToArray());

                //write string to file
                System.IO.File.WriteAllText(ItemsFolder, json);
                return "Ajout effectué";


            }
            else return "aucun objet";

        }

        public List<ItemLine> ListerProduit()
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(ItemsFolder))
            {

                string json = r.ReadToEnd();
                stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemLine>>(json);
            }
            return stock;
        }
    }
}
