using System;
using System.Collections.Generic;

namespace GameofLife
{
    public interface IGameUI
    {
        void GameMenu();
        void Clear();
        ConsoleKey ToggleInput();
        void GameIsSaved();
        void PrintList(GameList gameList, List<int> gamesLoaded);
        void PrintGame(GameList gameList);
        string UserAction();
        void ChooseGame(GameList gameList, List<int> gamesLoaded);
        void NoGameExists();
        void MaxGameList();
        //void WriteLine(string format, params object[] args);
    }
}
