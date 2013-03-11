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
            return "ID: " + this.ID + "\n"
                            + "Alcohol Type: " + this.AlcoholType + "\n"
                            + "Alcohol" + this.Alcohol + "\n"
                            + "Content" + this.Content + "\n"
                            + "Age" + this.Age + "\n"
                            + "Shell" + this.Shell + "\n"
                            + "Name" + this.Name + "\n"
                            + "Shape" + this.Shape + "\n"
                            + "Color" + this.Color + "\n"
                            + "Material" + this.Material + "\n"
                            + "Manufacturer" + this.Manufacturer + "\n"
                            + "City" + this.City + "\n"
                            + "Country" + this.Country + "\n"
                            + "Continent" + this.Continent + "\n"
                            + "Note" + this.Note + "\n";
        }

        public static string Serialize(Bottle b)
        {
            return b.ID.ToString().Replace('#', ' ') + "#"
                + b.AlcoholType.Replace('#', ' ') + "#"
                + b.Alcohol.Replace('#', ' ') + "#"
                + b.Content.Replace('#', ' ') + "#"
                + b.Age.ToString().Replace('#', ' ') + "#"
                + b.Shell.Replace('#', ' ') + "#"
                + b.Name.Replace('#', ' ') + "#"
                + b.Shape.Replace('#', ' ') + "#"
                + b.Color.Replace('#', ' ') + "#"
                + b.Material.Replace('#', ' ') + "#"
                + b.Manufacturer.Replace('#', ' ') + "#"
                + b.City.Replace('#', ' ') + "#"
                + b.Country.Replace('#', ' ') + "#"
                + b.Continent.Replace('#', ' ') + "#"
                + b.Note.Replace('#', ' ') + "#" + "\n";
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
            Bottle b = new Bottle();
            try
            {
                int testValue;
                if (int.TryParse(split[0], out testValue))
                {
                    b.ID = int.Parse(split[0]);
                }
                else
                {
                    throw new Exception("Invalid value for Age!");
                }                
                b.AlcoholType = split[1];
                b.Alcohol = split[2];
                b.Content = split[3];

                if (int.TryParse(split[4], out testValue))
                {
                    b.Age = int.Parse(split[4]);
                }
                else
                {
                    throw new Exception("Invalid value for Age!");
                }                
                b.Shell = split[5];
                b.Name = split[6];
                b.Shape = split[7];
                b.Color = split[8];
                b.Material = split[9];
                b.Manufacturer = split[10];
                b.City = split[11];
                b.Country = split[12];
                b.Continent = split[13];
                b.Note = split[14];
            }
            catch (IndexOutOfRangeException ex)
            {
                return null;
            }
            return b;         
        }        
    }
}
