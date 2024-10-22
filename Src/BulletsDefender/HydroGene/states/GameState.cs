// Decompiled with JetBrains decompiler
// Type: HydroGene.GameState
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

#nullable disable
namespace HydroGene
{
    public class GameState
    {
        protected MainGame mainGame;
        public GameState.SceneType CurrentSceneType;

        public Scene currentScene { get; set; }

        public GameState(MainGame mainGame) => this.mainGame = mainGame;

        public void ChangeScene(GameState.SceneType sceneType)
        {
            if (this.currentScene != null)
            {
                this.currentScene.Unload();
                this.currentScene = (Scene)null;
            }
            switch (sceneType)
            {
                case GameState.SceneType.Menu:
                    this.currentScene = (Scene)new SceneMenu();
                    break;
                case GameState.SceneType.Game:
                    this.currentScene = (Scene)new SceneGame();
                    break;
            }
            this.CurrentSceneType = sceneType;
            this.currentScene.Load();
        }

        public enum SceneType : byte
        {
            Menu,
            Game,
        }
    }
}
