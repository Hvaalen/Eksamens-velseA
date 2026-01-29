using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayGroundLib
{
    public class PlayGroundRepository
    {
        private List<PlayGround> playGrounds = new List<PlayGround>();
        private int nextId = 1;

        public PlayGroundRepository()
        {
            playGrounds.Add(new PlayGround(nextId++, "Millpark", 10, 5));
            playGrounds.Add(new PlayGround(nextId++, "Secret Playground", 12, 4));
            playGrounds.Add(new PlayGround(nextId++, "Library", 8, 3));
            playGrounds.Add(new PlayGround(nextId++, "School", 15, 7));

        }

        public List<PlayGround> GetAll()
        {
            return new List<PlayGround>(playGrounds);
        }

        public PlayGround? GetById(int id)
        {
            return playGrounds.FirstOrDefault(pg => pg.Id == id);
        }

        public PlayGround Add(PlayGround playGround)
        {
            playGround.Id = nextId++;
            playGrounds.Add(playGround);
            return playGround;
        }

        public PlayGround? Update(PlayGround playGround)
        {
            PlayGround? existingPlayGround = GetById(playGround.Id);
            if (existingPlayGround != null)
            {
                existingPlayGround.Name = playGround.Name;
                existingPlayGround.MaxChildren = playGround.MaxChildren;
                existingPlayGround.MinAge = playGround.MinAge;

                return existingPlayGround;
            }
            return null;
        }


    }
}
