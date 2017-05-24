using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BattleshipClass;

namespace Battleship_API.Models
{
    public sealed class ActiveGames
    {
        private static volatile ActiveGames instance;
        private static object syncRoot = new Object();
        public List<Game> listOfGames { get; set; }

        private ActiveGames()
        {
            listOfGames = new List<Game>();
        }

        public static ActiveGames Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ActiveGames();
                    }
                }

                return instance;
            }
        }
    }
}