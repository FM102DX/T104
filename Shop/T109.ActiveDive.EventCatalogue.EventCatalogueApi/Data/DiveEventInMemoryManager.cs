using System;
using System.Collections.Generic;
using System.Linq;
using T109.ActiveDive.Core;
using T109.ActiveDive.DataAccess;

namespace T109.ActiveDive.EventCatalogue.EventCatalogueApi
{
    public class DiveEventInMemoryManager
    {
        public InMemoryRepository<ActiveDiveEvent> Repository => repo; 

        public InMemoryRepository<ActiveDiveEvent> repo = new InMemoryRepository<ActiveDiveEvent>();

        string FullHttpAddress { get; set; }

        public DiveEventInMemoryManager(string fullHttpAddress)
        {
            FullHttpAddress = fullHttpAddress;
            Fill();
        }

        public IEnumerable<ActiveDiveEvent> GetAll ()
        {
            return repo.GetAll ();
        }

        public IEnumerable<ActiveDiveEvent> Search(string SearchText)
        {
            string[] words = SearchText.ToLower().Trim().Split(' ');

            List<ActiveDiveEvent> rez = new List<ActiveDiveEvent> ();
            List<ActiveDiveEvent> tmp = new List<ActiveDiveEvent>();

            foreach (string word in words)
            {
                
                tmp = Repository.Data.Where(x => x.Alias.ToLower().Contains(word) |
                                                 x.Name.ToLower().Contains(word) |
                                                 x.Description.ToLower().Contains(word)).ToList();
                tmp.ForEach(x => { rez.Add(x); });
            }
            return rez;
        }

        public List<ActiveDiveEvent> GetAllAsList()
        {
            return repo.GetItemsList();
        }

        public void Fill ()
        {

            repo.Add(new ActiveDiveEvent
            {
                Alias = "event1",
                Name = "Дайвинг на Флорипе",
                Description = "Дайвинг на Флорипе Description"
            }) ;
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event2",
                Name = "Дайвинг на Флорипе 2",
                Description = "Дайвинг на Флорипе Description 2"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event3",
                Name = "Дайвинг на Флорипе 3",
                Description = "Дайвинг на Флорипе Description 3"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event4",
                Name = "Alligator tour",
                Description = "Alligator tour for brave people"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event5",
                Name = "Killfish tour",
                Description = "Killfish tour - dive into St.Petersburg pubs"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event6",
                Name = "Lion safari",
                Description = "Lion safari on savanna with leons actually"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event7",
                Name = "Ice climber tour",
                Description = "Diving under ice"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event8",
                Name = "Spaceship diving",
                Description = "Dive into real scpace for real money"
            });
            repo.Add(new ActiveDiveEvent
            {
                Alias = "event9",
                Name = "Diving cources",
                Description = "Dive into real scpace for real money"
            });
            foreach (ActiveDiveEvent item in repo.GetAll())
            {
                item.FirstPic = $@"{FullHttpAddress}\EventData\{item.Alias}\1.jpg";
                item.SecondPic = $@"{FullHttpAddress}\EventData\{item.Alias}\2.jpg";
                item.ThirdPic = $@"{FullHttpAddress}\EventData\{item.Alias}\3.jpg";
            }
        }


    }
}
