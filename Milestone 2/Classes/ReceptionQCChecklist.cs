using System;
using System.Collections.Generic;

namespace Milestone_2.Classes
{
    class ReceptionQCChecklist
    {
        public int QCID { get; set; }
        public bool Validated { get; set; }
        public List<string> Items { get; set; }
        public List <int> Quantity { get; set; }   
        public DateTime Date { get; set; }
        public List<string> ItemDefects { get; set; }
        public List<string> ItemPictures { get; set; }

        public void CreateQCChecklist(List<string> items, List<string> itemDefects, List<string> itemPictures, List<int> quantity)
        {
            QCID = new Random().Next(1000, 9999);
            Items = items;
            Date = DateTime.Now;
            ItemDefects = itemDefects;
            ItemPictures = itemPictures;
            Quantity = quantity;
            Console.WriteLine("QC checklist created successfully.");
        }

        public void ValidateQCChecklist(bool validated)
        {
            Validated = validated;
        }

        public void EditQCChecklist(List<string> items, List<string> itemDefects, List<string> itemPictures, List<int> quantity)
        {
            Items = items;
            ItemDefects = itemDefects;
            ItemPictures = itemPictures;
            Quantity = quantity;
            Console.WriteLine("QC checklist edited successfully.");
        }
    }
}
