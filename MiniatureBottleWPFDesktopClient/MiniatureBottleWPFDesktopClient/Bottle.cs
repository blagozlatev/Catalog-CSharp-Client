using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniatureBottleWPFDesktopClient
{    
    public class Bottle
    {        
        public int ID { get; set; }
        public string AlcoholType { get; set; }
        public string Alcohol { get; set; }
        public string Content { get; set; }
        public int Age { get; set; }
        public string Shell { get; set; }
        public string Name { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Manufacturer { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            string strReturn = "Age: " + this.Age + "\n" + "Alcohol: " + this.Alcohol + "\n" +
                "Alcohol Type: " + this.AlcoholType + "\n" + "City: " + this.City + "\n" + "Color: " +
                this.Color + "\n" + "Content: " + this.Content + "\n" + "Continent: " + this.Continent + "\n" +
                "Country: " + this.Continent + "\n" + "ID: " + this.ID + "\n" + "Manufacturer: " +
                this.Manufacturer + "\n" + "Material: " + this.Material + "\n" + "Name: " + this.Name + "\n" +
                "Note: " + this.Note + "\n" + "Shape: " + this.Shape + "\n" + "Shell: " + this.Shell + "\n";
            return strReturn;
        }

        public string Serialize()
        {
            
            return this.Age + "#" + this.Alcohol + "#" + this.AlcoholType + "#" +
                        this.City + "#" + this.Color + "#" + this.Content + "#" + this.Continent + "#"
                        + this.Country + "#" + this.ID + "#" + this.Manufacturer + "#" + this.Material
                         + "#" + this.Name + "#" + this.Note + "#" + this.Shape + "#" + this.Shell + "\n";
        }        

        public static Bottle Deserialize(string serialized)
        {
            string[] split = serialized.Split('#');
            for (int i = 0; i < split.Count(); i++)
            {
                if (string.IsNullOrEmpty(split[i]))
                {
                    split[i] = string.Empty;
                }
            }
            Bottle bottle = new Bottle();
            try
            {
                int testValue;
                if (int.TryParse(split[0], out testValue))
                {
                    bottle.Age = int.Parse(split[0]);
                }
                else
                {
                    throw new Exception("Invalid value for Age!");
                }
                bottle.Alcohol = split[1];
                bottle.AlcoholType = split[2];
                bottle.City = split[3];
                bottle.Color = split[4];
                bottle.Content = split[5];
                bottle.Continent = split[6];
                bottle.Country = split[7];
                if (int.TryParse(split[8], out testValue))
                {
                    bottle.ID = int.Parse(split[8]);
                }
                else
                {
                    throw new Exception("Invalid value for ID!");
                }
                bottle.Manufacturer = split[9];
                bottle.Material = split[10];
                bottle.Name = split[11];
                bottle.Note = split[12];
                bottle.Shape = split[13];
                bottle.Shell = split[14];
            }
            catch (IndexOutOfRangeException ex)
            {
                return null;
            }
            return bottle;
        }        
    }
}
