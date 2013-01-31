using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace MiniatureBottleDesktopClient
{
    public partial class MainForm : Form
    {
        Byte[] pic = GetPhoto();
        public MainForm()
        {
            InitializeComponent();
        }

        public static byte[] GetPhoto()
        {
            FileStream stream = new FileStream(
               "C:\\Users\\Blagovest Zlatev\\Downloads\\lala\\lala.jpg"
               , FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MiniatureBottlesDataSet dataset = new MiniatureBottlesDataSet();            
            MiniatureBottlesDataSet.BottleDataTable bottle_table = new MiniatureBottlesDataSet.BottleDataTable();            
            dataGridView1.DataSource = bottle_table;
            MiniatureBottlesDataSet.BottleRow row = bottle_table.NewBottleRow();
            row["ID"] = 21;            
            row["Shelf"] = "La";
            row["Name"] = "La";
            row["Shape"] = "La";
            row["Material"] = "La";
            row["Age"] = 2;
            row["Note"] = "La";
            row["BottleImage"] = GetPhoto();
            bottle_table.AddBottleRow(row);            
            dataset.AcceptChanges();
            var image = Image.FromFile("C:\\Users\\Blagovest Zlatev\\Downloads\\lala\\lala.jpg");
            var newImage = ScaleImage(image, 300, 250);

            picBox.Image = newImage;
        }
    }
}
